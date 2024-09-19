using System.Text.Json.Serialization;

namespace ZingMP3Explode.Songs
{
    /// <summary>
    /// Information about the preview of a song that is locked behind the paywall.
    /// </summary>
    public class PreviewInfo
    {
        /// <summary>
        /// The start time of the preview in seconds.
        /// </summary>
        [JsonPropertyName("startTime")]
        public long StartTime { get; set; }

        /// <summary>
        /// The end time of the preview in seconds.
        /// </summary>
        [JsonPropertyName("endTime")]
        public long EndTime { get; set; }
    }
}
