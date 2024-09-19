using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Albums;
using ZingMP3Explode.Artists;
using ZingMP3Explode.Bridge;
using ZingMP3Explode.Enums;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Interfaces;
using ZingMP3Explode.Playlists;
using ZingMP3Explode.Songs;
using ZingMP3Explode.Utilities;
using ZingMP3Explode.Videos;

namespace ZingMP3Explode.Search
{
    //2do: rewrite
    public class SearchClient
    {
        ZingMP3Endpoint endpoint;

        public SearchClient(ZingMP3Endpoint endpoint)
        {
            this.endpoint = endpoint;
        }

        /// <summary>
        /// Checks for valid url.
        /// </summary>
        /// <param name="url"></param>
        public bool IsUrlValid(string url) => Uri.IsWellFormedUriString(url, UriKind.Absolute);

        /// <summary>
        /// Enumerates search results returned by the specified query.
        /// </summary>
        public async IAsyncEnumerable<ISearchable?> GetResultsAsyncEnumerable(string query, SearchFilter filter, int page = Constants.DEFAULT_PAGE, int count = Constants.DEFAULT_LIMIT, bool allowCorrect = true, [EnumeratorCancellation] CancellationToken token = default)
        {
            Validate(count);
            string type = filter.GetName();

            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "q", query },
                { "type", type },
                { "page", page.ToString() },
                { "count", count.ToString() },
                { "allowCorrect", Convert.ToByte(allowCorrect).ToString() },
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/search", parameters, token);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            //var correctKeyword = node["correctKeyword"].GetValue<string?>();
            var data = node["items"].AsArray();

            foreach (var item in data)
            {
                string? link = item["link"].GetValue<string>();
                if (link == null || !Uri.IsWellFormedUriString(Constants.ZINGMP3_LINK.TrimEnd('/') + link, UriKind.Absolute))
                    continue;
                link = Constants.ZINGMP3_LINK.TrimEnd('/') + link;
                var linkUri = new Uri(link);
                if (linkUri.Segments[1] == "nghe-si/")
                    yield return item.Deserialize<Artist>();
                if (linkUri.Segments[1] == "bai-hat/")
                    yield return item.Deserialize<Song>();
                if (linkUri.Segments[1] == "album/")
                {
                    var baseList = item.Deserialize<BasePlaylist>(JsonDefaults.Options)!;
                    if (baseList.IsAlbum)
                        baseList = new Album(baseList);
                    else
                        baseList = new Playlist(baseList);
                    yield return baseList;
                }
                if (linkUri.Segments[1] == "video-clip/")
                    yield return item.Deserialize<Video>(JsonDefaults.Options);
            }
        }

        public async IAsyncEnumerable<T> GetResultsAsyncEnumerable<T>(string query, int page = Constants.DEFAULT_PAGE, int count = Constants.DEFAULT_LIMIT, bool allowCorrect = true, [EnumeratorCancellation] CancellationToken token = default) where T : ISearchable
        {
            SearchFilter filter;
            if (typeof(T) == typeof(Artist))
                filter = SearchFilter.Artist;
            else if (typeof(T) == typeof(Song))
                filter = SearchFilter.Song;
            else if (typeof(T) == typeof(Album) || typeof(T) == typeof(Playlist))
                filter = SearchFilter.PlaylistAndAlbums;
            else if (typeof(T) == typeof(Video))
                filter = SearchFilter.Video;
            else
                throw new ZingMP3ExplodeException("Invalid search type");
            await foreach (var item in GetResultsAsyncEnumerable(query, filter, page, count, allowCorrect, token))
            {
                if (item is T t)
                    yield return t;
            }
        }

        public async Task<List<ISearchable?>> GetResultsAsync(string query, SearchFilter filter, int page = Constants.DEFAULT_PAGE, int count = Constants.DEFAULT_LIMIT, bool allowCorrect = true, CancellationToken token = default)
        {
            List<ISearchable?> results = new List<ISearchable?>();
            await foreach (var item in GetResultsAsyncEnumerable(query, filter, page, count, allowCorrect, token))
                results.Add(item);
            return results;
        }

        public async Task<List<T>> GetResultsAsync<T>(string query, int page = Constants.DEFAULT_PAGE, int count = Constants.DEFAULT_LIMIT, bool allowCorrect = true, CancellationToken token = default) where T : ISearchable
        {
            List<T> results = new List<T>();
            await foreach (var item in GetResultsAsyncEnumerable<T>(query, page, count, allowCorrect, token))
                results.Add(item);
            return results;
        }

        static void Validate(int limit)
        {
            if (limit < Constants.MIN_LIMIT || limit > Constants.MAX_LIMIT)
                throw new ZingMP3ExplodeException($"Limit must be between {Constants.MIN_LIMIT} and {Constants.MAX_LIMIT}");
        }
    }
}
