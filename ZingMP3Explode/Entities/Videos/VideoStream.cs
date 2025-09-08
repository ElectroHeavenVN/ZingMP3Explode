using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about a video stream.</para>
    /// <para xml:lang="vi">Thông tin về một luồng video.</para>
    /// </summary>
    public class VideoStream
    {
        [JsonConstructor]
        internal VideoStream() { }

        [JsonInclude, JsonPropertyName("hls")]
        internal Dictionary<string, string> hls { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">Dictionary containing HLS stream URLs with their quality as the key (e.g., "720p", "1080p", "0p").</para>
        /// <para xml:lang="vi">Từ điển chứa các URL luồng HLS với chất lượng làm khóa (ví dụ: "720p", "1080p", "0p").</para>
        /// </summary>
        [JsonIgnore]
#if NETFRAMEWORK
        public ReadOnlyDictionary<string, string> HLS => new(hls);
#else
        public ReadOnlyDictionary<string, string> HLS => hls.AsReadOnly();
#endif
        /// <summary>
        /// <para xml:lang="en">Gets the best available HLS stream URL based on quality preference (1080p > 720p > 360p > 0p).</para>
        /// <para xml:lang="vi">Lấy URL luồng HLS có chất lượng tốt nhất dựa trên ưu tiên chất lượng (1080p > 720p > 360p > 0p).</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">The best available HLS stream URL, or an empty string if none are available.</para>
        /// <para xml:lang="vi">URL luồng HLS có chất lượng tốt nhất, hoặc chuỗi rỗng nếu không có luồng nào khả dụng.</para>
        /// </returns>
        public string GetBestHLS()
        {
            if (hls.TryGetValue("1080p", out string? result) && !string.IsNullOrEmpty(result))
                return result;
            if (hls.TryGetValue("720p", out result) && !string.IsNullOrEmpty(result))
                return result;
            if (hls.TryGetValue("360p", out result) && !string.IsNullOrEmpty(result))
                return result;
            if (hls.TryGetValue("0p", out result) && !string.IsNullOrEmpty(result))
                return result;
            return "";
        }

        //maybe there are more types of stream here
    }
}
