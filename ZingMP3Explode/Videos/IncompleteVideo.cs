using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZingMP3Explode.Artists;
using ZingMP3Explode.Enums;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Videos
{
    public class IncompleteVideo : IZingMP3Object, ISearchable
    {
        [JsonPropertyName("encodeId")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        [JsonPropertyName("isOffical")]
        public bool IsOfficial { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("artistsNames")]
        public string AllArtistsNames { get; set; }

        [JsonPropertyName("artists")]
        public List<IncompleteArtist>? Artists { get; set; }

        [JsonPropertyName("isWorldWide")]
        public bool IsWorldWide { get; set; }

        [JsonPropertyName("thumbnailM")]
        public string ThumbnailM { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        /// <summary>
        /// The streaming status of the video.
        /// </summary>
        [JsonPropertyName("streamingStatus")]
        public SubscriptionType StreamingStatus { get; set; }

        /// <summary>
        /// The stream privileges of the video.
        /// </summary>
        [JsonPropertyName("streamPrivileges")]
        public SubscriptionType[] StreamPrivileges { get; set; } = [];

        [JsonPropertyName("artist")]
        public IncompleteArtist? Artist { get; set; }
    }
}
