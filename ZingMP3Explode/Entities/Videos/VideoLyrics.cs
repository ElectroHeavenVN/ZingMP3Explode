using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about the lyrics of a video.</para>
    /// <para xml:lang="vi">Thông tin về lời bài hát của một video.</para>
    /// </summary>
    public class VideoLyrics : IZingMP3Object
    {
        [JsonConstructor]
        internal VideoLyrics() { }

        /// <summary>
        /// <para xml:lang="en">ID of the lyrics.</para>
        /// <para xml:lang="vi">ID của lời bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("id")]
        public string ID { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Content of the lyrics.</para>
        /// <para xml:lang="vi">Nội dung của lời bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("content")]
        public string Content { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Username of the person who contributed the lyrics.</para>
        /// <para xml:lang="vi">Tên người dùng của người đóng góp lời bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("username")]
        public string Username { get; internal set; } = "";
    }
}
