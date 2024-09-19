using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZingMP3Explode.Albums;
using ZingMP3Explode.Artists;
using ZingMP3Explode.Genres;
using ZingMP3Explode.Songs;

namespace ZingMP3Explode.Videos
{
    public class Video : IncompleteVideo
    {
        [JsonPropertyName("startTime")]
        public long StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public long EndTime { get; set; }

        [JsonPropertyName("statusName")]
        public string StatusName { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("createdAt")]
        public long CreatedAt { get; set; }

        [JsonPropertyName("disabledAds")]
        public bool IsAdsDisabled { get; set; }

        [JsonPropertyName("privacy")]
        public string PrivacyStatus { get; set; }

        [JsonPropertyName("lyric")]
        public string Lyrics { get; set; }

        [JsonPropertyName("song")]
        public Song Song { get; set; }

        [JsonPropertyName("genres")]
        public List<IncompleteGenre> Genres { get; set; }

        [JsonPropertyName("composers")]
        public List<IncompleteArtist> Composers { get; set; }

        [JsonPropertyName("album")]
        public Album Album { get; set; }

        [JsonPropertyName("lyrics")]
        public List<VideoLyrics> VideoLyrics { get; set; }

        [JsonPropertyName("recommends")]
        public List<IncompleteVideo> RecommendedVideos { get; set; }

        [JsonPropertyName("like")]
        public int Likes { get; set; }

        [JsonPropertyName("listen")]
        public int Listens { get; set; }

        [JsonPropertyName("liked")]
        public bool Liked { get; set; }

        [JsonPropertyName("comment")]
        public int Comments { get; set; }

        [JsonPropertyName("streaming")]
        public VideoStream VideoStream { get; set; }
    }
}
