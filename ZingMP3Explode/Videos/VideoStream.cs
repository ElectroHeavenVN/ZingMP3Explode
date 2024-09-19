using System.Text.Json.Serialization;

namespace ZingMP3Explode.Videos
{
    public class VideoStream
    {
        public class HLSVideoStream
        {
            [JsonPropertyName("0p")]
            public string? StreamLink { get; set; }
        }

        [JsonPropertyName("hls")]
        public HLSVideoStream? HLS { get; set; }

        //maybe there are more types of stream here
    }
}
