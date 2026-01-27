using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Weekly leaderboard information for regions.</para>
    /// <para xml:lang="vi">Thông tin bảng xếp hạng tuần của các khu vực.</para>
    /// </summary>
    public class WeeklyLeaderboards
    {
        [JsonConstructor]
        internal WeeklyLeaderboards() { }

        /// <summary>
        /// <para xml:lang="en">Vietnamese leaderboard.</para>
        /// <para xml:lang="vi">Bảng xếp hạng Việt Nam.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("vn")]
        public WeeklyLeaderboard Vietnam { get; internal set; } = new WeeklyLeaderboard();

        /// <summary>
        /// <para xml:lang="en">US-UK leaderboard.</para>
        /// <para xml:lang="vi">Bảng xếp hạng US-UK.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("us")]
        public WeeklyLeaderboard USUK { get; internal set; } = new WeeklyLeaderboard();

        /// <summary>
        /// <para xml:lang="en">K-Pop leaderboard.</para>
        /// <para xml:lang="vi">Bảng xếp hạng K-Pop.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("korea")]
        public WeeklyLeaderboard KPop { get; internal set; } = new WeeklyLeaderboard();
    }
}