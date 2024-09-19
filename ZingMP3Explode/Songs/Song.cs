using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ZingMP3Explode.Albums;
using ZingMP3Explode.Artists;
using ZingMP3Explode.Enums;
using ZingMP3Explode.Genres;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Songs
{
    /// <summary>
    /// Information of a song.
    /// </summary>
    public class Song : IZingMP3Object, ISearchable
    {
        [JsonPropertyName("releaseDate")]
        long releaseDateUnix;

        /// <inheritdoc/>
        [JsonPropertyName("encodeId")]
        public string Id { get; set; }

        /// <summary>
        /// The URL of the song.
        /// </summary>
        [JsonPropertyName("link")]
        public string Link { get; set; }

        /// <summary>
        /// Short URL of the song. The URL will be redirected to the full URL.
        /// </summary>
        public string ShortUrl => $"https://zingmp3.vn/bai-hat/{Id}.html";

        /// <inheritdoc />
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Alias of the song (the string before the ID in the <see cref="Link"/>).
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Indicate whether the song is official.
        /// </summary>
        [JsonPropertyName("isOffical")] // Typo in the API
        public bool IsOfficial { get; set; }

        /// <summary>
        /// Username of the uploader if the song is uploaded by a user.
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; }

        /// <summary>
        /// All artists' names separated by commas.
        /// </summary>
        [JsonPropertyName("artistsNames")]
        public string AllArtistsNames { get; set; }

        /// <summary>
        /// List of artists associated with the song.
        /// </summary>
        [JsonPropertyName("artists")]
        public List<IncompleteArtist>? Artists { get; set; }

        /// <summary>
        /// Indicate whether the song is world-wide or the song is only available in Vietnam.
        /// </summary>
        [JsonPropertyName("isWorldWide")]
        public bool IsWorldWide { get; set; }

        /// <summary>
        /// Preview of the song if the song is locked behind the paywall.
        /// </summary>
        [JsonPropertyName("previewInfo")]
        public PreviewInfo? PreviewInfo { get; set; }

        /// <summary>
        /// URL of the big thumbnail of the song.
        /// </summary>
        [JsonPropertyName("thumbnailM")]
        public string ThumbnailM { get; set; }

        /// <summary>
        /// URL of the small thumbnail of the song.
        /// </summary>
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        /// Duration of the song in seconds.
        /// </summary>
        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        /// <summary>
        /// Indicate whether the song is a Zing Choice song.
        /// </summary>
        [JsonPropertyName("zingChoice")]
        public bool IsZingChoice { get; set; }

        /// <summary>
        /// Indicate whether the song is private.
        /// </summary>
        [JsonPropertyName("isPrivate")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Indicate whether the song is a pre-release song.
        /// </summary>
        [JsonPropertyName("preRelease")]
        public bool IsPreRelease { get; set; }

        /// <summary>
        /// The release date of the song in UTC.
        /// </summary>
        public DateTime ReleaseDateUTC => DateTimeOffset.FromUnixTimeSeconds(releaseDateUnix).DateTime;

        /// <summary>
        /// Gerne IDs of the song.
        /// </summary>
        [JsonPropertyName("genreIds")]
        public List<string>? GenreIds { get; set; }

        /// <summary>
        /// Album of the song.
        /// </summary>
        [JsonPropertyName("album")]
        public Album? Album { get; set; }

        /// <summary>
        /// Name of the song's distributor.
        /// </summary>
        [JsonPropertyName("distributor")]
        public string Distributor { get; set; }

        /// <summary>
        /// List of indicators of the song, for example "explicit".
        /// </summary>
        [JsonPropertyName("indicators")]
        public List<string> Indicators { get; set; }

        /// <summary>
        /// Radio ID of the song.
        /// </summary>
        [JsonPropertyName("radioId")]
        public long RadioId { get; set; }

        /// <summary>
        /// Indicate whether the song is indie.
        /// </summary>
        [JsonPropertyName("isIndie")]
        public bool IsIndie { get; set; }

        /// <summary>
        /// URL of the MV of the song.
        /// </summary>
        [JsonPropertyName("mvlink")]
        public string MVLink { get; set; }

        /// <summary>
        /// The streaming status of the song.
        /// </summary>
        [JsonPropertyName("streamingStatus")]
        public SubscriptionType StreamingStatus { get; set; }

        /// <summary>
        /// The stream privileges of the song.
        /// </summary>
        [JsonPropertyName("streamPrivileges")]
        public SubscriptionType[] StreamPrivileges { get; set; } = [];

        /// <summary>
        /// The download privileges of the song.
        /// </summary>
        [JsonPropertyName("downloadPrivileges")]
        public SubscriptionType[] DownloadPrivileges { get; set; } = [];

        /// <summary>
        /// Indicate if audio ads can be played.
        /// </summary>
        [JsonPropertyName("allowAudioAds")]
        public bool AllowAudioAds { get; set; }

        /// <summary>
        /// Indicate if the song has lyrics.
        /// </summary>
        [JsonPropertyName("hasLyric")]
        public bool HasLyric { get; set; }

        /// <summary>
        /// The user ID of the uploader.
        /// </summary>
        [JsonPropertyName("userid")]
        public long UserId { get; set; }

        /// <summary>
        /// Genres of the song.
        /// </summary>
        [JsonPropertyName("genres")]
        public List<Genre>? Genres { get; set; }

        /// <summary>
        /// Composers of the song.
        /// </summary>
        [JsonPropertyName("composers")]
        public List<Composer>? Composers { get; set; }

        /// <summary>
        /// Indicate if the song can be registered as a ring back tone.
        /// </summary>
        [JsonPropertyName("isRBT")]
        public bool IsRBT { get; set; }

        /// <summary>
        /// Numbers of likes of the song.
        /// </summary>
        [JsonPropertyName("like")]
        public long Likes { get; set; }

        /// <summary>
        /// Numbers of listens of the song.
        /// </summary>
        [JsonPropertyName("listen")]
        public long Listens { get; set; }

        /// <summary>
        /// Indicate if the song is liked by the user.
        /// </summary>
        [JsonPropertyName("liked")]
        public bool Liked { get; set; }

        /// <summary>
        /// Number of comments of the song.
        /// </summary>
        [JsonPropertyName("comment")]
        public long Comments { get; set; }

        /// <summary>
        /// the ranking status of the song in the new songs leaderboard. Negative values indicate the rank of the song is decreasing and vice versa.
        /// </summary>
        [JsonPropertyName("rakingStatus")]
        public int RankingStatus { get; set; }

#pragma warning disable CS1591 // Unknown JSON properties
        [JsonPropertyName("publicStatus")]
        public int PublicStatus { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("statusName")]
        public string StatusName { get; set; }
        [JsonPropertyName("uname")]
        public string UName { get; set; }
        [JsonPropertyName("canEdit")]
        public bool CanEdit { get; set; }
        [JsonPropertyName("canDelete")]
        public bool CanDelete { get; set; }
#pragma warning restore CS1591

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"Song ({Title})";
    }
}
