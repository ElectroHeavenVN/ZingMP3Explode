using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">#zingchart leaderboard data.</para>
    /// <para xml:lang="vi">Dữ liệu bảng xếp hạng #zingchart.</para>
    /// </summary>
    public class ZingChartData
    {
        [JsonConstructor]
        internal ZingChartData() { }

        /// <summary>
        /// <para xml:lang="en">Real-time chart information.</para>
        /// <para xml:lang="vi">Thông tin biểu đồ bảng xếp hạng thời gian thực.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("RTChart")]
        public RealTimeChart RealTimeChart { get; internal set; } = new RealTimeChart();

        [JsonInclude, JsonPropertyName("newRelease")]
        internal List<Song> newRelease { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of new release songs.</para>
        /// <para xml:lang="vi">Danh sách các bài hát phát hành mới.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<Song> NewlyReleasedSongs => newRelease.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Weekly leaderboard information.</para>
        /// <para xml:lang="vi">Thông tin bảng xếp hạng tuần.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("weekChart")]
        public WeeklyLeaderboards WeeklyLeaderboard { get; internal set; } = new WeeklyLeaderboards();
    }
}