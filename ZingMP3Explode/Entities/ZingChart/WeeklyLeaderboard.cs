using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Weekly leaderboard information.</para>
    /// <para xml:lang="vi">Thông tin bảng xếp hạng tuần.</para>
    /// </summary>
    public class WeeklyLeaderboard : Section<Song>
    {
        [JsonConstructor]
        internal WeeklyLeaderboard() { }

        /// <summary>
        /// <para xml:lang="en">Banner URL of the leaderboard.</para>
        /// <para xml:lang="vi">URL biểu ngữ của bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("banner")]
        public string BannerUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Playlist ID of the leaderboard.</para>
        /// <para xml:lang="vi">ID danh sách phát của đến bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("playlistId")]
        public string PlaylistId { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Chart ID of the leaderboard.</para>
        /// <para xml:lang="vi">ID biểu đồ của bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("chartId")]
        public int ChartId { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Cover URL of the leaderboard.</para>
        /// <para xml:lang="vi">URL ảnh bìa của bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("cover")]
        public string CoverUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Country of the leaderboard.</para>
        /// <para xml:lang="vi">Quốc gia của bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("country")]
        public string Country { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Type of the leaderboard.</para>
        /// <para xml:lang="vi">Loại bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("type")]
        public string Type { get; internal set; } = "";

        [JsonInclude, JsonPropertyName("group")]
        internal List<WeeklyLeaderboardGroup> groups { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of leaderboard groups.</para>
        /// <para xml:lang="vi">Danh sách các nhóm bảng xếp hạng.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<WeeklyLeaderboardGroup> Groups => groups.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Link to the leaderboard.</para>
        /// <para xml:lang="vi">Liên kết đến bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("link")]
        public string Link { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Week number of the leaderboard.</para>
        /// <para xml:lang="vi">Số tuần của bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("week")]
        public int Week { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Year of the leaderboard.</para>
        /// <para xml:lang="vi">Năm của bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("year")]
        public int Year { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Latest week number in the leaderboard.</para>
        /// <para xml:lang="vi">Số tuần mới nhất trong bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("latestWeek")]
        public int LatestWeek { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Start date of the leaderboard.</para>
        /// <para xml:lang="vi">Ngày bắt đầu của bảng xếp hạng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("startDate")]
        public string StartDate { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">End date of the leaderboard.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("endDate")]
        public string EndDate { get; internal set; } = "";
    }
}