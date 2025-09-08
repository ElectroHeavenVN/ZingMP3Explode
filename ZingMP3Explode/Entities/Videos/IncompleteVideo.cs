using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Basic information about a video/MV.</para>
    /// <para xml:lang="vi">Thông tin cơ bản về một video/MV.</para>
    /// </summary>
    public class IncompleteVideo : IZingMP3Object, ISearchable
    {
        [JsonConstructor]
        internal IncompleteVideo() { }

        /// <summary>
        /// <para xml:lang="en">ID of the video.</para>
        /// <para xml:lang="vi">ID của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("encodeId")]
        public string ID { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Title of the video.</para>
        /// <para xml:lang="vi">Tiêu đề của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("title")]
        public string Title { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Alias of the video.</para>
        /// <para xml:lang="vi">Bí danh của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("alias")]
        public string Alias { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Whether the video is official.</para>
        /// <para xml:lang="vi">Video có phải là chính thức không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isOffical")]
        public bool IsOfficial { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Username of the uploader.</para>
        /// <para xml:lang="vi">Tên người đăng tải.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("username")]
        public string Username { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">All artists' names, separated by commas.</para>
        /// <para xml:lang="vi">Tên tất cả nghệ sĩ, cách nhau bằng dấu phẩy.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("artistsNames")]
        public string AllArtistsNames { get; internal set; } = "";

        [JsonInclude, JsonPropertyName("artists")]
        internal List<IncompleteArtist> artists { get; set; } = [];

        /// <summary>
        /// <para xml:lang="en">List of artists.</para>
        /// <para xml:lang="vi">Danh sách nghệ sĩ.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<IncompleteArtist> Artists => artists.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Whether the video is available worldwide.</para>
        /// <para xml:lang="vi">Video có khả dụng toàn cầu không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isWorldWide")]
        public bool IsWorldWide { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">URL of the big thumbnail of the video.</para>
        /// <para xml:lang="vi">Đường dẫn ảnh thu nhỏ lớn của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("thumbnailM")]
        public string BigThumbnailUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Url to the video.</para>
        /// <para xml:lang="vi">Liên kết đến video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("link")]
        public string Url { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">URL of the thumbnail of the video.</para>
        /// <para xml:lang="vi">Đường dẫn ảnh thu nhỏ của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("thumbnail")]
        public string ThumbnailUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Duration of the video in seconds.</para>
        /// <para xml:lang="vi">Thời lượng video (giây).</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("duration")]
        public long Duration { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The streaming status of the video.</para>
        /// <para xml:lang="vi">Trạng thái phát trực tuyến của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("streamingStatus")]
        public SubscriptionType StreamingStatus { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The stream privileges of the video.</para>
        /// <para xml:lang="vi">Quyền phát trực tuyến của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("streamPrivileges")]
        public SubscriptionType[] StreamPrivileges { get; internal set; } = [];

        /// <summary>
        /// <para xml:lang="en">Main artist of the video.</para>
        /// <para xml:lang="vi">Nghệ sĩ chính của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("artist")]
        public IncompleteArtist? Artist { get; internal set; }
    }
}
