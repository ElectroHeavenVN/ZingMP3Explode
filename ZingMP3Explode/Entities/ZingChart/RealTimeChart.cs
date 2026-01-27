using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Real-time chart data of #zingchart leaderboard.</para>
    /// <para xml:lang="vi">Dữ liệu biểu đồ thời gian thực của bảng xếp hạng #zingchart.</para>
    /// </summary>
    public class RealTimeChart : Section<Song>
    {
        [JsonConstructor]
        internal RealTimeChart() { }

        /// <summary>
        /// <para xml:lang="en">Type of the chart.</para>
        /// <para xml:lang="vi">Loại biểu đồ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("chartType")]
        public string ChartType { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Real-time chart graph information of the leaderboard.</para>
        /// <para xml:lang="vi">Thông tin đồ thị thời gian thực của biểu đồ bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("chart")]
        public RealTimeChartGraph ChartGraph { get; internal set; } = new RealTimeChartGraph();

        [JsonInclude, JsonPropertyName("promotes")]
        internal List<Song> promotes { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of song recommendations.</para>
        /// <para xml:lang="vi">Danh sách các bài hát được đề xuất.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<Song> SongRecommendations => promotes.AsReadOnly();
    }
}