using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Bridge;
using ZingMP3Explode.Utilities;
using ZingMP3Explode.Videos;

namespace ZingMP3Explode.User
{
    public class UserClient
    {
        ZingMP3Endpoint endpoint;

        public UserClient(ZingMP3Endpoint endpoint)
        {
            this.endpoint = endpoint;
        }

        public async Task<LoggedInUser?> GetAsync(CancellationToken cancellationToken = default)
        {
            string resolvedJson = await endpoint.GetAsync("/api/v2/user/profile/get/info", null, cancellationToken);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<LoggedInUser>(JsonDefaults.Options);
        }

        public async Task<FavoriteSongList> GetFavoriteSongsAsync(int page = Constants.DEFAULT_PAGE, int count = Constants.DEFAULT_LIMIT, CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "library" },
                { "page", page.ToString() },
                { "count", count.ToString() },
                { "sectionId", "mFavSong" }
            };
            string resolvedJson = await endpoint.GetAsync("/api/v2/user/song/get/list", parameters, cancellationToken);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<FavoriteSongList>(JsonDefaults.Options);
        }

        public async Task<FavoriteAlbumList> GetFavoriteAlbumsAsync(int page = Constants.DEFAULT_PAGE, int count = Constants.DEFAULT_LIMIT, CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "library" },
                { "page", page.ToString() },
                { "count", count.ToString() },
                { "sectionId", "mAlbum" }
            };
            string resolvedJson = await endpoint.GetAsync("/api/v2/user/album/get/list", parameters, cancellationToken);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<FavoriteAlbumList>(JsonDefaults.Options);
        }

        public async Task<VideoList> GetFavoriteMVsAsync(int page = Constants.DEFAULT_PAGE, int count = Constants.DEFAULT_LIMIT, CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "library" },
                { "page", page.ToString() },
                { "count", count.ToString() },
                { "sectionId", "mMV" }
            };
            string resolvedJson = await endpoint.GetAsync("/api/v2/user/video/get/list", parameters, cancellationToken);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<VideoList>(JsonDefaults.Options);
        }

        public async Task<BlockedSongList> GetBlockedSongsAsync(CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "song" }
            };
            string resolvedJson = await endpoint.GetAsync("/api/v2/user/assets/get/blocked-assets", parameters, cancellationToken);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<BlockedSongList>(JsonDefaults.Options);
        }

        public async Task<BlockedArtistsList> GetBlockedArtistsAsync(CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "artist" }
            };
            string resolvedJson = await endpoint.GetAsync("/api/v2/user/assets/get/blocked-assets", parameters, cancellationToken);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<BlockedArtistsList>(JsonDefaults.Options);
        }
    }
}
