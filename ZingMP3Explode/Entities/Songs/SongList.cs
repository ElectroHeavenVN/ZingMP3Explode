using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">A list of songs with additional information.</para>
    /// <para xml:lang="vi">Danh sách các bài hát kèm theo thông tin bổ sung.</para>
    /// </summary>
    public class SongList
    {
        [JsonConstructor]
        internal SongList() { }

        [JsonInclude, JsonPropertyName("items")]
        internal List<Song> items { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of songs.</para>
        /// <para xml:lang="vi">Danh sách các bài hát.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<Song> Items => items.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Total number of songs in the list.</para>
        /// <para xml:lang="vi">Tổng số bài hát trong danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("total")]
        public int Total { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Total duration of all songs in the list in seconds.</para>
        /// <para xml:lang="vi">Tổng thời lượng của tất cả các bài hát trong danh sách (tính bằng giây).</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("totalDuration")]
        public long TotalDuration { get; internal set; }
    }
}
