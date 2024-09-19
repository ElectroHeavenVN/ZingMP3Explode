using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Videos
{
    public class VideoLyrics : IZingMP3Object
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
