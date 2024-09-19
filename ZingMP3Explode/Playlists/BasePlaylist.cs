using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZingMP3Explode.Artists;
using ZingMP3Explode.Genres;
using ZingMP3Explode.Interfaces;
using ZingMP3Explode.Songs;

namespace ZingMP3Explode.Playlists
{
    /// <summary>
    /// Base class for albums and playlists.
    /// </summary>
    public class BasePlaylist : IZingMP3Object, ISearchable
    {
        [JsonPropertyName("releasedAt")]
        internal long releaseDateUnix;

        /// <inheritdoc/>
        [JsonPropertyName("encodeId")]
        public string Id { get; set; }

        /// <summary>
        /// Title of the list.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// URL of the big thumbnail of the list.
        /// </summary>
        [JsonPropertyName("thumbnailM")]
        public string ThumbnailM { get; set; }

        /// <summary>
        /// URL of the small thumbnail of the list.
        /// </summary>
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        /// Indicates if the list is official.
        /// </summary>
        [JsonPropertyName("isoffical")]
        public bool IsOfficial { get; set; }

        /// <summary>
        /// URL of the list.
        /// </summary>
        [JsonPropertyName("link")]
        public string Link { get; set; }

        /// <summary>
        /// Indicates if the list is indie.
        /// </summary>
        [JsonPropertyName("isIndie")]
        public bool IsIndie { get; set; }

        /// <summary>
        /// The release date of the list.
        /// </summary>
        [JsonPropertyName("releaseDate")]
        public string ReleaseDate { get; set; }

        /// <summary>
        /// Short description of the list.
        /// </summary>
        [JsonPropertyName("sortDescription")] // This is a typo in the API.
        public string ShortDescription { get; set; }

        /// <summary>
        /// Description of the list.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The release date of the list in UTC.
        /// </summary>
        public DateTime ReleaseDateUTC => DateTimeOffset.FromUnixTimeSeconds(releaseDateUnix).DateTime;

        /// <summary>
        /// Genre IDs of the list.
        /// </summary>
        [JsonPropertyName("genreIds")]
        public List<string>? GenreIds { get; set; }

        /// <summary>
        /// List of artists associated with the list.
        /// </summary>
        [JsonPropertyName("artists")]
        public List<IncompleteArtist>? Artists { get; set; }

        /// <summary>
        /// All artists' names separated by commas.
        /// </summary>
        [JsonPropertyName("artistsNames")]
        public string AllArtistsNames { get; set; }

        /// <summary>
        /// UID of the list.
        /// </summary>
        [JsonPropertyName("uid")]
        public int Uid { get; set; }

        /// <summary>
        /// Indicates if the list is private.
        /// </summary>
        [JsonPropertyName("isPrivate")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Username of the uploader if the song is uploaded by a user.
        /// </summary>
        [JsonPropertyName("userName")]
        public string Username { get; set; }

        /// <summary>
        /// Indicates if the list is an album.
        /// </summary>
        [JsonPropertyName("isAlbum")]
        public bool IsAlbum { get; set; }

        /// <summary>
        /// The type of the list.
        /// </summary>
        [JsonPropertyName("textType")]
        public string? Type { get; set; }

        /// <summary>
        /// Indicates if the list is a single.
        /// </summary>
        [JsonPropertyName("isSingle")]
        public bool? IsSingle { get; set; }

        /// <summary>
        /// Name of the list's distributor.
        /// </summary>
        [JsonPropertyName("distributor")]
        public string Distributor { get; set; }

        /// <summary>
        /// Alias of the list (the string before the ID in the <see cref="Link"/>).
        /// </summary>
        [JsonPropertyName("aliasTitle")]
        public string Alias { get; set; }

        /// <summary>
        /// The last update timestamp of the list's content.
        /// </summary>
        [JsonPropertyName("contentLastUpdate")]
        public long? ContentLastUpdate { get; set; }

        /// <summary>
        /// Genres of the list.
        /// </summary>
        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        /// <summary>
        /// List of songs in the list.
        /// </summary>
        [JsonPropertyName("song")]
        public SongList? Songs { get; set; }

        /// <summary>
        /// Number of likes of the list.
        /// </summary>
        [JsonPropertyName("like")]
        public long? Likes { get; set; }

        /// <summary>
        /// Number of listens of the list.
        /// </summary>
        [JsonPropertyName("listen")]
        public long? Listens { get; set; }

        /// <summary>
        /// Indicates if the list is liked by the user.
        /// </summary>
        [JsonPropertyName("liked")]
        public bool Liked { get; set; }

#pragma warning disable CS1591 // Unknown JSON properties
        [JsonPropertyName("playItemMode")]
        public int PlayItemMode { get; set; }
        [JsonPropertyName("subType")]
        public int SubType { get; set; }
        [JsonPropertyName("isShuffle")]
        public bool IsShuffle { get; set; }
        [JsonPropertyName("sectionId")]
        public string? SectionId { get; set; }
        [JsonPropertyName("PR")]
        public bool IsPR { get; set; }
#pragma warning restore CS1591
    }
}
