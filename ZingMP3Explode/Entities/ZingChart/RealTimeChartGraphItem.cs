using System;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Real-time chart graph information of #zingchart leaderboard.</para>
    /// <para xml:lang="vi">Thông tin giá trị đồ thị thời gian thực của bảng xếp hạng #zingchart.</para>
    /// </summary>
    public class RealTimeChartGraphItem
    {
        [JsonConstructor]
        internal RealTimeChartGraphItem() { }

        [JsonInclude, JsonPropertyName("time")]
        internal long timeUnix;
        /// <summary>
        /// <para xml:lang="en">Time of the graph item.</para>
        /// <para xml:lang="vi">Thời gian của giá trị đồ thị.</para>
        /// </summary>
        [JsonIgnore]
        public DateTime Time => DateTimeOffset.FromUnixTimeSeconds(timeUnix).UtcDateTime;

        /// <summary>
        /// <para xml:lang="en">Hour of the graph item.</para>
        /// <para xml:lang="vi">Giờ của giá trị đồ thị.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("hour")]
        public string Hour { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Counter value of the graph item.</para>
        /// <para xml:lang="vi">Giá trị bộ đếm của giá trị đồ thị.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("counter")]
        public int Counter { get; internal set; }
    }
}