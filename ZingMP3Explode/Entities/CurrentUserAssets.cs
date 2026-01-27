using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="vi">Thông tin về nội dung yêu thích và bị chặn của người dùng hiện tại.</para>
    /// <para xml:lang="en">Information about the current user's favorited and blocked assets.</para>
    /// </summary>
    public class CurrentUserAssets
    {
        [JsonConstructor]
        internal CurrentUserAssets() { }

        /// <summary>
        /// <para xml:lang="en">IDs of playlists favorited by the current user.</para>
        /// <para xml:lang="vi">ID của các playlist đã được người dùng hiện tại yêu thích.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("playlistsFavourited")]
        public string[] FavoritePlaylistsIDs { get; internal set; } = [];

        /// <summary>
        /// <para xml:lang="en">IDs of videos favorited by the current user.</para>
        /// <para xml:lang="vi">ID của các video đã được người dùng hiện tại yêu thích.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("videosFavourited")]
        public string[] FavoriteVideosIDs { get; internal set; } = [];

        /// <summary>
        /// <para xml:lang="en">IDs of songs favorited by the current user.</para>
        /// <para xml:lang="vi">ID của các bài hát đã được người dùng hiện tại yêu thích.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("songsFavourited")]
        public string[] FavoriteSongsIDs { get; internal set; } = [];

        /// <summary>
        /// <para xml:lang="en">IDs of medias blocked by the current user.</para>
        /// <para xml:lang="vi">ID của các nội dung bị người dùng hiện tại chặn.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("mediasBlocked")]
        public string[] BlockedMediaIDs { get; internal set; } = [];

        /// <summary>
        /// <para xml:lang="en">IDs of artists blocked by the current user.</para>
        /// <para xml:lang="vi">ID của các nghệ sĩ bị người dùng hiện tại chặn.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("artistsBlocked")]
        public string[] BlockedArtistsIDs { get; internal set; } = [];
    }
}
