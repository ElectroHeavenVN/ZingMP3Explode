using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Basic information about an artist.</para>
    /// <para xml:lang="vi">Thông tin cơ bản về một nghệ sĩ.</para>
    /// </summary>
    public class IncompleteArtist : IZingMP3Object, ISearchable
    {
        [JsonConstructor]
        internal IncompleteArtist() { }

        /// <summary>
        /// <para xml:lang="en">ID of the artist.</para>
        /// <para xml:lang="vi">ID của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("id")]
        public string ID { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Name of the artist.</para>
        /// <para xml:lang="vi">Tên của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("name")]
        public string Name { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Url to the artist's page.</para>
        /// <para xml:lang="vi">Liên kết đến trang của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("link")]
        public string Url { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Whether the artist is in the spotlight.</para>
        /// <para xml:lang="vi">Nghệ sĩ có đang nổi bật hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("spotlight")]
        public bool IsSpotlight { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Alias of the artist.</para>
        /// <para xml:lang="vi">Bí danh của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("alias")]
        public string Alias { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Url to the small thumbnail of the artist.</para>
        /// <para xml:lang="vi">Liên kết đến ảnh đại diện nhỏ của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("thumbnail")]
        public string ThumbnailUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Url to the big thumbnail of the artist.</para>
        /// <para xml:lang="vi">Liên kết đến ảnh đại diện lớn của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("thumbnailM")]
        public string BigThumbnailUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Whether the artist is an official account.</para>
        /// <para xml:lang="vi">Nghệ sĩ có phải là tài khoản chính thức hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isOA")]
        public bool IsOA { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Whether the artist is an official account brand.</para>
        /// <para xml:lang="vi">Nghệ sĩ có phải là tài khoản chính thức của một thương hiệu hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isOABrand")]
        public bool IsOABrand { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">ID of the artist's top songs playlist.</para>
        /// <para xml:lang="vi">ID danh sách phát tuyển tập các bài hát hay nhất của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("playlistId")]
        public string TopSongsPlaylistID { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Number of followers of the artist.</para>
        /// <para xml:lang="vi">Số lượng người theo dõi nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("totalFollow")]
        public long TotalFollow { get; internal set; }
    }
}
