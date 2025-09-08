using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json.Serialization;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about a video.</para>
    /// <para xml:lang="vi">Thông tin về một video.</para>
    /// </summary>
    public class Video : IncompleteVideo
    {
        [JsonConstructor]
        internal Video() { }

        /// <summary>
        /// <para xml:lang="en">Start time of the video in seconds.</para>
        /// <para xml:lang="vi">Thời điểm bắt đầu của video (tính bằng giây).</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("startTime")]
        public long StartTime { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">End time of the video in seconds.</para>
        /// <para xml:lang="vi">Thời điểm kết thúc của video (tính bằng giây).</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("endTime")]
        public long EndTime { get; internal set; }

        [JsonInclude, JsonPropertyName("createdAt")]
        internal long createdAt { get; set; }
        /// <summary>
        /// <para xml:lang="en">The UTC creation date of the video.</para>
        /// <para xml:lang="vi">Ngày tạo video theo giờ UTC.</para>
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedAtUTC => DateTimeOffset.FromUnixTimeSeconds(createdAt).UtcDateTime;

        /// <summary>
        /// <para xml:lang="en">Indicates whether ads are disabled for this video.</para>
        /// <para xml:lang="vi">Cho biết quảng cáo có bị tắt cho video này không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("disabledAds")]
        public bool IsAdsDisabled { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Privacy status of the video.</para>
        /// <para xml:lang="vi">Trạng thái quyền riêng tư của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("privacy")]
        public string PrivacyStatus { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Relative path to the lyrics of the video.</para>
        /// <para xml:lang="vi">Đường dẫn tương đối đến lời bài hát của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("lyric")]
        public string LyricsUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">The full URL to the lyrics of the video.</para>
        /// <para xml:lang="vi">URL đầy đủ đến lời bài hát của video.</para>
        /// </summary>
        [JsonIgnore]
        public string FullLyricsUrl => Constants.ZINGMP3_LINK.TrimEnd('/') + (Path.GetDirectoryName(Url) ?? "").Replace(Path.DirectorySeparatorChar, '/') + '/' + LyricsUrl;

        /// <summary>
        /// <para xml:lang="en">Song information associated with the video.</para>
        /// <para xml:lang="vi">Thông tin bài hát liên quan đến video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("song")]
        public Song? Song { get; internal set; }

        [JsonInclude, JsonPropertyName("genres")]
        internal List<IncompleteGenre> genres { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of genres of the video.</para>
        /// <para xml:lang="vi">Danh sách thể loại của video.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<IncompleteGenre> Genres => genres.AsReadOnly();

        [JsonInclude, JsonPropertyName("composers")]
        internal List<IncompleteArtist> composers { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of composers of the video.</para>
        /// <para xml:lang="vi">Danh sách nhạc sĩ sáng tác video.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<IncompleteArtist> Composers => composers.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Album information associated with the video.</para>
        /// <para xml:lang="vi">Thông tin album liên quan đến video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("album")]
        public Album? Album { get; internal set; }

        [JsonInclude, JsonPropertyName("lyrics")]
        internal List<VideoLyrics> videoLyrics { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of video lyrics.</para>
        /// <para xml:lang="vi">Danh sách lời bài hát cho video.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<VideoLyrics> VideoLyrics => videoLyrics.AsReadOnly();

        [JsonInclude, JsonPropertyName("recommends")]
        internal List<IncompleteVideo> recommendedVideos { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of recommended videos.</para>
        /// <para xml:lang="vi">Danh sách video được đề xuất.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<IncompleteVideo> RecommendedVideos => recommendedVideos.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Number of likes for the video.</para>
        /// <para xml:lang="vi">Số lượt thích của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("like")]
        public int Likes { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Number of listens for the video.</para>
        /// <para xml:lang="vi">Số lượt nghe của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("listen")]
        public int Listens { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Indicates if the video is liked by the current user.</para>
        /// <para xml:lang="vi">Video có được người dùng hiện tại thích hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("liked")]
        public bool Liked { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Number of comments for the video.</para>
        /// <para xml:lang="vi">Số bình luận của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("comment")]
        public int Comments { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Streaming information for the video.</para>
        /// <para xml:lang="vi">Thông tin phát trực tuyến của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("streaming")]
        public VideoStream VideoStream { get; internal set; } = new VideoStream();

        /// <summary>
        /// <para xml:lang="en">Status name of the video.</para>
        /// <para xml:lang="vi">Tên trạng thái của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("statusName")]
        public string StatusName { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Status code of the video.</para>
        /// <para xml:lang="vi">Mã trạng thái của video.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("statusCode")]
        public int StatusCode { get; internal set; }
    }
}
