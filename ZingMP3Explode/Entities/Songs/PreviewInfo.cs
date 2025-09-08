using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about the preview of a song that is locked behind the paywall.</para>
    /// <para xml:lang="vi">Thông tin về phần xem trước của bài hát yêu cầu trả phí.</para>
    /// </summary>
    public class PreviewInfo
    {
        [JsonConstructor]
        internal PreviewInfo() { }

        /// <summary>
        /// <para xml:lang="en">The start time of the preview in seconds.</para>
        /// <para xml:lang="vi">Thời gian bắt đầu của phần xem trước (tính bằng giây).</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("startTime")]
        public long StartTime { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The end time of the preview in seconds.</para>
        /// <para xml:lang="vi">Thời gian kết thúc của phần xem trước (tính bằng giây).</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("endTime")]
        public long EndTime { get; internal set; }
    }
}
