using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about the number of results in each category of a multi-category search.</para>
    /// <para xml:lang="vi">Thông tin về số lượng kết quả trong từng loại của một tìm kiếm đa loại.</para>
    /// </summary>
    public class MultiSearchResultCounter
    {
        [JsonConstructor]
        internal MultiSearchResultCounter() { }

        /// <summary>
        /// <para xml:lang="en">Number of songs in the search result.</para>
        /// <para xml:lang="vi">Số lượng bài hát trong kết quả tìm kiếm.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("song")]
        public int Songs { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Number of artists in the search result.</para>
        /// <para xml:lang="vi">Số lượng nghệ sĩ trong kết quả tìm kiếm.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("artist")]
        public int Artists { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Number of videos in the search result.</para>
        /// <para xml:lang="vi">Số lượng video trong kết quả tìm kiếm.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("video")]
        public int Videos { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Number of playlists in the search result.</para>
        /// <para xml:lang="vi">Số lượng danh sách phát trong kết quả tìm kiếm.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("playlist")]
        public int Playlists { get; internal set; }
    }
}
