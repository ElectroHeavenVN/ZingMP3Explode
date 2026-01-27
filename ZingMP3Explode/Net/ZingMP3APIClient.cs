#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Entities;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Interfaces;
using ZingMP3Explode.SourceGen;
using ZingMP3Explode.Utilities;

// TODO: Hubs, top 100

namespace ZingMP3Explode.Net
{
    /// <summary>
    /// <para xml:lang="en">Client for accessing ZingMP3 API.</para>
    /// <para xml:lang="vi">Client để truy cập API của ZingMP3.</para>
    /// </summary>
    public class ZingMP3APIClient
    {
        readonly ZingMP3Client zClient;
        HttpClient http => zClient.HttpClient;

        internal ZingMP3APIClient(ZingMP3Client client)
        {
            zClient = client;
        }

        public async Task<Album> GetAlbumAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "page/get/playlist";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "id", id },
                { "thumbSize", "600_600" }
            };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.Album) ?? throw new ZingMP3ExplodeException($"Cannot get album with id {id}.");
        }

        public async Task<Artist> GetArtistByAliasAsync(string alias, CancellationToken cancellationToken = default)
        {
            string path = "page/get/artist";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "alias", alias } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.Artist) ?? throw new ZingMP3ExplodeException($"Cannot get artist with alias {alias}.");
        }

        public async IAsyncEnumerable<Song> GetSongsAsync(string artistID, SortType sortType = SortType.Popular, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string path = "song/get/list";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "id", artistID },
                { "type", "artist" },
                { "page", 1 },
                { "count", 30 },
                { "sort", sortType.GetName() },
                { "sectionId", "aSongs" },
            };
            bool hasMore = true;
            while (hasMore)
            {
                JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
                hasMore = node["hasMore"].GetBoolValue();
                List<Song> songs = node["items"].Deserialize(SourceGenerationContext.Default.ListSong) ?? throw new ZingMP3ExplodeException($"Cannot get songs of artist with id {artistID}.");
                if (songs.Count == 0)
                    yield break;
                foreach (var song in songs)
                    yield return song;
                parameters["page"] = (int)parameters["page"] + 1;
                if (!hasMore)
                    yield break;
            }
        }

        public async IAsyncEnumerable<IncompleteVideo> GetMVsAsync(string artistID, SortType sortType = SortType.Popular, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string path = "video/get/list";
            if (!Regexes.ArtistID.IsMatch(artistID))
                throw new ZingMP3ExplodeException("Invalid artist id");
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "id", artistID },
                { "type", "artist" },
                { "page", 1 },
                { "count", 30 },
                { "sort", sortType.GetName() }
            };
            bool hasMore = true;
            while (hasMore)
            {
                JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
                hasMore = node["hasMore"].GetBoolValue();
                List<IncompleteVideo> videos = node["items"].Deserialize(SourceGenerationContext.Default.ListIncompleteVideo) ?? throw new ZingMP3ExplodeException($"Cannot get MVs of artist with id {artistID}.");
                if (videos.Count == 0)
                    yield break;
                foreach (var video in videos)
                    yield return video;
                parameters["page"] = (int)parameters["page"] + 1;
                if (!hasMore)
                    yield break;
            }
        }

        public async Task<Genre> GetGerneAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "genre/get/info";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "id", id },
                { "type", "album" }
            };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.Genre) ?? throw new ZingMP3ExplodeException($"Cannot get genre with id {id}.");
        }

        public async Task<Playlist> GetPlaylistAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "page/get/playlist";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "id", id },
                { "thumbSize", "600_600" }
            };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.Playlist) ?? throw new ZingMP3ExplodeException($"Cannot get playlist with id {id}.");
        }

        public async Task<MultiSearchResult> SearchMultiAsync(string query, bool allowCorrection, CancellationToken cancellationToken)
        {
            string path = "search/multi";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "q", query },
                { "allowCorrect", Convert.ToByte(allowCorrection) }
            };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.MultiSearchResult) ?? throw new ZingMP3ExplodeException($"Cannot search with query {query}.");
        }

        public async IAsyncEnumerable<ISearchable> SearchAsync(string query, SearchFilter filter, bool allowCorrection = true, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string path = "search";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "q", query },
                { "type", filter.GetName() },
                { "page", 1 },
                { "count", 18 },
                { "allowCorrect", Convert.ToByte(allowCorrection) },
            };
            int total = 0;
            int fetched = 0;
            do
            {
                JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
                foreach (var item in node["items"]?.AsArray() ?? [])
                {
                    switch (filter)
                    {
                        case SearchFilter.Song:
                            yield return item.Deserialize(SourceGenerationContext.Default.Song) ?? throw new ZingMP3ExplodeException("Failed to deserialize song.");
                            break;
                        case SearchFilter.Artist:
                            yield return item.Deserialize(SourceGenerationContext.Default.Artist) ?? throw new ZingMP3ExplodeException("Failed to deserialize artist.");
                            break;
                        case SearchFilter.PlaylistAndAlbums:
                            var baseList = item.Deserialize(SourceGenerationContext.Default.BaseList) ?? throw new ZingMP3ExplodeException("Failed to deserialize playlist/album.");
                            if (baseList.IsAlbum)
                                yield return new Album(baseList);
                            else
                                yield return new Playlist(baseList);
                            break;
                        case SearchFilter.Video:
                            yield return item.Deserialize(SourceGenerationContext.Default.Video) ?? throw new ZingMP3ExplodeException("Failed to deserialize video.");
                            break;
                    }
                }
                total = node["total"].GetIntValue();
                fetched += node["items"]?.AsArray().Count ?? 0;
                parameters["page"] = (int)parameters["page"] + 1;
                if (fetched >= total)
                    yield break;
            }
            while (fetched < total);
        }

        public async Task<Song> GetSongAsync(string id, CancellationToken cancellationToken = default)
        {
            //string path = "page/get/song";
            string path = "song/get/info";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.Song) ?? throw new ZingMP3ExplodeException($"Cannot get song with id {id}.");
        }

        public async Task<string> GetAudioStreamUrlAsync(string id, AudioQuality quality = AudioQuality.Best, CancellationToken cancellationToken = default)
        {
            string path = "song/get/streaming";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            string normalQuality = node["128"].GetStringValue();
            string highQuality = node["320"].GetStringValue();
            string losslessQuality = node["lossless"].GetStringValue();
            if (quality == AudioQuality.Best)
            {
                if (!string.IsNullOrEmpty(losslessQuality))
                    return losslessQuality;
                if (!string.IsNullOrEmpty(highQuality) && highQuality != "VIP")
                    return highQuality;
                if (!string.IsNullOrEmpty(normalQuality))
                    return normalQuality;
                throw new ZingMP3ExplodeException("The song has no stream link.");
            }
            if (!string.IsNullOrEmpty(losslessQuality) && quality == AudioQuality.Lossless)
                return losslessQuality;
            if (!string.IsNullOrEmpty(highQuality) && quality == AudioQuality.High)
                return highQuality;
            if (!string.IsNullOrEmpty(normalQuality) && quality == AudioQuality.Normal)
                return normalQuality;
            throw new ZingMP3ExplodeException("The song has no stream link with the requested quality.");
        }

        public async Task<Dictionary<AudioQuality, string>> GetAllAudioStreamUrlsAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "song/get/streaming";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            Dictionary<AudioQuality, string> result = [];
            if (node.AsObject().ContainsKey("128"))
                result.Add(AudioQuality.Normal, node["128"].GetStringValue());
            if (node.AsObject().ContainsKey("320"))
                result.Add(AudioQuality.High, node["320"].GetStringValue());
            if (node.AsObject().ContainsKey("lossless"))
                result.Add(AudioQuality.Lossless, node["lossless"].GetStringValue());
            return result;
        }

        public async Task<LyricData> GetLyricsAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "lyric/get/lyric";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            LyricData lyricData = node.Deserialize(SourceGenerationContext.Default.LyricData) ?? throw new ZingMP3ExplodeException($"Cannot get lyrics of song with id {id}.");
            if (lyricData.File is not null)
            {
                HttpResponseMessage message = await http.GetAsync(lyricData.File, cancellationToken);
                if (message.IsSuccessStatusCode)
                    lyricData.SyncedLyrics = await message.Content.ReadAsStringAsync(cancellationToken);
            }
            return lyricData;
        }

        public async Task<CurrentUser> GetCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            string path = "user/profile/get/info";
            JsonNode node = await SendGetAndCheckErrorCode(path, null, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.CurrentUser) ?? throw new ZingMP3ExplodeException("Cannot get current user.");
        }

        public async IAsyncEnumerable<Song> GetMyFavoriteSongsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string path = "user/song/get/list";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "type", "library" },
                { "page", 1 },
                { "count", 50 },
                { "sectionId", "mFavSong" }
            };
            int fetched = 0;
            int total = 0;
            do
            {
                JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
                total = node["total"].GetIntValue();
                foreach (var item in node["items"]?.AsArray() ?? [])
                {
                    var song = item.Deserialize(SourceGenerationContext.Default.Song) ?? throw new ZingMP3ExplodeException("Failed to deserialize song.");
                    yield return song;
                    fetched++;
                }
                parameters["page"] = (int)parameters["page"] + 1;
                if (fetched >= total)
                    yield break;
            }
            while (fetched < total);
        }

        public async IAsyncEnumerable<Album> GetMyFavoriteAlbumsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string path = "user/album/get/list";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "type", "library" },
                { "page", 1 },
                { "count", 50 },
                { "sectionId", "mAlbum" }
            };
            int fetched = 0;
            int total = 0;
            do
            {
                JsonNode album = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
                total = album["total"].GetIntValue();
                foreach (var item in album["items"]?.AsArray() ?? [])
                {
                    var song = item.Deserialize(SourceGenerationContext.Default.Album) ?? throw new ZingMP3ExplodeException("Failed to deserialize song.");
                    yield return song;
                    fetched++;
                }
                parameters["page"] = (int)parameters["page"] + 1;
                if (fetched >= total)
                    yield break;
            }
            while (fetched < total);
        }

        public async IAsyncEnumerable<IncompleteVideo> GetMyFavoriteMVsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string path = "user/video/get/list";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "type", "library" },
                { "page", 1 },
                { "count", 50 },
                { "sectionId", "mMV" }
            };
            int fetched = 0;
            int total = 0;
            do
            {
                JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
                total = node["total"].GetIntValue();
                foreach (var item in node["items"]?.AsArray() ?? [])
                {
                    var video = item.Deserialize(SourceGenerationContext.Default.IncompleteVideo) ?? throw new ZingMP3ExplodeException("Failed to deserialize video.");
                    yield return video;
                    fetched++;
                }
                parameters["page"] = (int)parameters["page"] + 1;
                if (fetched >= total)
                    yield break;
            }
            while (fetched < total);
        }

        public async IAsyncEnumerable<Song> GetMyBlockedSongsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string path = "user/assets/get/blocked-assets";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "type", "song" },
                { "page", 1 },
                { "count", 50 }
            };
            int fetched = 0;
            int total = 0;
            do
            {
                JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
                total = node["total"].GetIntValue();
                foreach (var item in node["items"]?.AsArray() ?? [])
                {
                    var song = item.Deserialize(SourceGenerationContext.Default.Song) ?? throw new ZingMP3ExplodeException("Failed to deserialize song.");
                    yield return song;
                    fetched++;
                }
                parameters["page"] = (int)parameters["page"] + 1;
                if (fetched >= total)
                    yield break;
            }
            while (fetched < total);
        }

        public async IAsyncEnumerable<IncompleteArtist> GetMyBlockedArtistsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string path = "user/assets/get/blocked-assets";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "type", "artist" },
                { "page", 1 },
                { "count", 50 }
            };
            int fetched = 0;
            int total = 0;
            do
            {
                JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
                total = node["total"].GetIntValue();
                foreach (var item in node["items"]?.AsArray() ?? [])
                {
                    var artist = item.Deserialize(SourceGenerationContext.Default.IncompleteArtist) ?? throw new ZingMP3ExplodeException("Failed to deserialize artist.");
                    yield return artist;
                    fetched++;
                }
                parameters["page"] = (int)parameters["page"] + 1;
                if (fetched >= total)
                    yield break;
            }
            while (fetched < total);
        }

        public async Task<Video> GetVideoAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "page/get/video";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.Video) ?? throw new ZingMP3ExplodeException($"Cannot get video with id {id}.");
        }

        public async Task<List<PageSection<Video>>> GetVideoRelatedSectionsAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "video/get/section-relate";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.ListPageSectionVideo) ?? throw new ZingMP3ExplodeException($"Cannot get related section of video with id {id}.");
        }

        public async Task<CurrentUserAssets> GetCurrentUserAssetsAsync(CancellationToken cancellationToken = default)
        {
            string path = "user/assets/get/assets";
            JsonNode node = await SendGetAndCheckErrorCode(path, null, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.CurrentUserAssets) ?? throw new ZingMP3ExplodeException("Cannot get current user assets.");
        }

        public async Task<List<Song>> GetSongsRecommendationsAsync(int count, CancellationToken cancellationToken = default)
        {
            string path = "song/get/section-song-station";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "count", count } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node["items"]?.Deserialize(SourceGenerationContext.Default.ListSong) ?? throw new ZingMP3ExplodeException("Cannot get song recommendations.");
        }

        public async Task<List<Album>> GetRecentAlbumsAsync(CancellationToken cancellationToken = default)
        {
            string path = "user/recent-play-home";
            JsonNode node = await SendGetAndCheckErrorCode(path, null, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.ListAlbum) ?? throw new ZingMP3ExplodeException("Cannot get recent albums.");
        }

        public async Task<ZingChartData> GetZingChartAsync(CancellationToken cancellationToken = default)
        {
            string path = "page/get/chart-home";
            JsonNode node = await SendGetAndCheckErrorCode(path, null, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.ZingChartData) ?? throw new ZingMP3ExplodeException("Cannot get current Zing chart.");
        }
        
        public async Task<WeeklyLeaderboard> GetWeeklyLeaderboardRegionAsync(string id, int week, int year, CancellationToken cancellationToken = default)
        {
            string path = "page/get/week-chart";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "id", id },
                { "week", week },
                { "year", year }
            };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.WeeklyLeaderboard) ?? throw new ZingMP3ExplodeException($"Cannot get weekly leaderboard region with id {id} for week {week}, year {year}.");
        }

        public async Task<PageSection<Song>> GetNewlyReleasedSongsAsync(CancellationToken cancellationToken = default)
        {
            string path = "page/get/newrelease-chart";
            JsonNode node = await SendGetAndCheckErrorCode(path, null, cancellationToken);
            return node.Deserialize(SourceGenerationContext.Default.PageSectionSong) ?? throw new ZingMP3ExplodeException("Cannot get newly released songs.");
        }

        public async Task<JsonNode> CallAPIAsync(string path, Dictionary<string, object>? parameters, CancellationToken cancellationToken = default)
        {
            return await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
        }

        async Task<JsonNode> SendGetAndCheckErrorCode(string path, Dictionary<string, object>? parameters, CancellationToken cancellationToken)
        {
            parameters ??= [];
            var pr = GetCommonParams();
            foreach (var param in parameters)
                pr.Add(param.Key, param.Value);
            pr.Add("sig", Sign(path, pr));
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, Constants.ZINGMP3_LINK.TrimEnd('/') + Constants.API_BASE_PATH + path.TrimStart('/'), pr);
            HttpResponseMessage response = await http.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            Utils.CheckErrorCode(await response.Content.ReadAsStringAsync(cancellationToken), out JsonNode result);
            return result;
        }

        Dictionary<string, object> GetCommonParams() => new Dictionary<string, object>()
        {
            { "ctime", DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
            { "version", zClient.Version },
            { "apiKey", zClient.APIKey }
        };

        string Sign(string path, Dictionary<string, object> parameters)
        {
            var hashParams = parameters
                .Where(p => Constants.HASH_PARAMS.Contains(p.Key))
                .Select(p => $"{p.Key}={p.Value}")
                .ToList();
            hashParams.Sort(string.Compare);
            string hash = Constants.API_BASE_PATH + path.TrimStart('/') + Utils.HashSHA256(string.Join("", hashParams));
            string sig = Utils.HashSHA512(hash, zClient.Secret);
            return sig;
        }

        static HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, Dictionary<string, object> parameters)
        {
            endpoint = endpoint.TrimEnd('/');
            string urlParam = Utils.ChainParams(parameters);
            HttpRequestMessage request = new HttpRequestMessage(method, endpoint + "?" + urlParam);
            return request;
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member