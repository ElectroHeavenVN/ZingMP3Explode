using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Real-time chart graph information of #zingchart leaderboard.</para>
    /// <para xml:lang="vi">Thông tin đồ thị thời gian thực của bảng xếp hạng #zingchart.</para>
    /// </summary>
    public class RealTimeChartGraph
    {
        [JsonConstructor]
        internal RealTimeChartGraph() { }

        [JsonIgnore]
        internal List<string> times { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of hours in the chart graph.</para>
        /// <para xml:lang="vi">Danh sách các giờ trong đồ thị biểu đồ.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> Hours => times.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Minimum score in the chart graph.</para>
        /// <para xml:lang="vi">Điểm số tối thiểu trong đồ thị biểu đồ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("minScore")]
        public double MinScore { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Maximum score in the chart graph.</para>
        /// <para xml:lang="vi">Điểm số tối đa trong đồ thị biểu đồ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("maxScore")]
        public double MaxScore { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Total score in the chart graph.</para>
        /// <para xml:lang="vi">Tổng điểm số trong đồ thị biểu đồ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("totalScore")]
        public double TotalScore { get; internal set; }

        [JsonInclude, JsonPropertyName("items")]
        internal Dictionary<string, RealTimeChartGraphItem[]> items { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">Dictionary containing the graph values of the top 3 songs in the chart, with the key being the song ID.</para>
        /// <para xml:lang="vi">Từ điển chứa giá trị đồ thị của top 3 bài hát trong biểu đồ, với khóa là ID bài hát.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyDictionary<string, RealTimeChartGraphItem[]> Items => items.AsReadOnly();
    }
}