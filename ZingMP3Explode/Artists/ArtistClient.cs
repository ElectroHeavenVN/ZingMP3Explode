using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Bridge;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Utilities;
using ZingMP3Explode.Songs;
using ZingMP3Explode.Videos;

namespace ZingMP3Explode.Artists
{
    public class ArtistClient
    {
        ZingMP3Endpoint endpoint;
        static readonly Regex urlRegex = new Regex(@"zingmp3\.vn\/(?:nghe-si\/)?(.*)", RegexOptions.Compiled);
        static readonly Regex idRegex = new Regex(@"^I[A-Z0-9]{7}$", RegexOptions.Compiled);
        static readonly Regex aliasRegex = new Regex(@"^[a-zA-Z0-9.-]*$", RegexOptions.Compiled);

        public ArtistClient(ZingMP3Endpoint endpoint)
        {
            this.endpoint = endpoint;
        }

        public bool IsUrlValid(string url) => IsUrlValid(url, out _);

        bool IsUrlValid(string url, out string alias)
        {
            alias = "";
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;
            Match match = urlRegex.Match(url);
            if (match.Success)
            {
                alias = match.Groups[1].Value;
                return true;
            }
            return false;
        }

        public async ValueTask<Artist?> GetAsync(string url)
        {
            if (!IsUrlValid(url, out string alias))
                throw new ZingMP3ExplodeException("Invalid artist url");
            return await GetByAliasAsync(alias);
        }

        public async ValueTask<Artist?> GetByAliasAsync(string alias, CancellationToken cancellationToken = default)
        {
            if (!aliasRegex.IsMatch(alias))
                throw new ZingMP3ExplodeException("Invalid artist alias");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "alias", alias }
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/page/get/artist", parameters, cancellationToken);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<Artist>(JsonDefaults.Options);
        }

        public async ValueTask<SongList?> GetSongsAsync(string artistId, int page = Constants.DEFAULT_PAGE, int count = Constants.DEFAULT_LIMIT, string sort = "listen", CancellationToken cancellationToken = default)
        {
            if (!idRegex.IsMatch(artistId))
                throw new ZingMP3ExplodeException("Invalid artist id");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", artistId },
                { "type", "artist" },
                { "page", page.ToString() },
                { "count", count.ToString() },
                { "sort", sort },
                { "sectionId", "aSongs" },
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/song/get/list", parameters, cancellationToken);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<SongList>(JsonDefaults.Options);
        }

        public async ValueTask<VideoList?> GetMVsAsync(string artistId, int page = Constants.DEFAULT_PAGE, int count = Constants.DEFAULT_LIMIT, SortType sortType = SortType.Popular, CancellationToken cancellationToken = default)
        {
            if (!idRegex.IsMatch(artistId))
                throw new ZingMP3ExplodeException("Invalid artist id");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", artistId },
                { "type", "artist" },
                { "page", page.ToString() },
                { "count", count.ToString() },
                { "sort", sortType.GetName() }
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/video/get/list", parameters, cancellationToken);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<VideoList>(JsonDefaults.Options);
        }

        public async ValueTask<List<Song>> GetAllSongsAsync(string artistId, CancellationToken cancellationToken = default)
        {
            List<Song> songs = new List<Song>();
            int page = 1;
            SongList? songList;
            do
            {
                songList = await GetSongsAsync(artistId, page++, count: Constants.MAX_LIMIT, cancellationToken: cancellationToken);
                if (songList == null)
                    break;
                songs.AddRange(songList.Items);
            } while (songList.HasMore);
            return songs;
        }

        public async ValueTask<List<IncompleteVideo>> GetAllMVsAsync(string artistId, CancellationToken cancellationToken = default)
        {
            List<IncompleteVideo> videos = new List<IncompleteVideo>();
            int page = 1;
            VideoList? videoList;
            do
            {
                videoList = await GetMVsAsync(artistId, page++, count: Constants.MAX_LIMIT, cancellationToken: cancellationToken);
                if (videoList == null)
                    break;
                videos.AddRange(videoList.Items);
            } while (videoList.HasMore);
            return videos;
        }
    }
}
