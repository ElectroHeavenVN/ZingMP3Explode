using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Basic information about a genre.</para>
    /// <para xml:lang="vi">Thông tin cơ bản về một thể loại.</para>
    /// </summary>
    public class IncompleteGenre : IZingMP3Object
    {
        [JsonConstructor]
        internal IncompleteGenre() { }

        /// <summary>
        /// <para xml:lang="en">The genre ID.</para>
        /// <para xml:lang="vi">ID của thể loại.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("id")]
        public string ID { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Name of the genre.</para>
        /// <para xml:lang="vi">Tên của thể loại.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("name")]
        public string Name { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Title of the genre.</para>
        /// <para xml:lang="vi">Tiêu đề của thể loại.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("title")]
        public string Title { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Alias of the genre.</para>
        /// <para xml:lang="vi">Bí danh của thể loại.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("alias")]
        public string Alias { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">The relative URL of the genre.</para>
        /// <para xml:lang="vi">URL tương đối của thể loại.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("link")]
        public string Url { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">The full URL of the genre.</para>
        /// <para xml:lang="vi">URL đầy đủ của thể loại.</para>
        /// </summary>
        [JsonIgnore]
        public string FullUrl => Constants.ZINGMP3_LINK.TrimEnd('/') + Url;
    }
}
