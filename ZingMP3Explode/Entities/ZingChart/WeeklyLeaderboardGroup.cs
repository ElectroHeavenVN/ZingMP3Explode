using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about a group in the weekly leaderboard.</para>
    /// <para xml:lang="vi">Thông tin về một nhóm trong bảng xếp hạng tuần.</para>
    /// </summary>
    public class WeeklyLeaderboardGroup
    {
        [JsonConstructor]
        internal WeeklyLeaderboardGroup() { }

        /// <summary>
        /// <para xml:lang="en">ID of the group.</para>
        /// <para xml:lang="vi">ID của nhóm.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("id")]
        public int Id { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Name of the group.</para>
        /// <para xml:lang="vi">Tên của nhóm.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("name")]
        public string Name { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Type of the group.</para>
        /// <para xml:lang="vi">Loại của nhóm.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("type")]
        public string Type { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Link to the group.</para>
        /// <para xml:lang="vi">Liên kết đến nhóm.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("link")]
        public string Link { get; internal set; } = "";
    }
}