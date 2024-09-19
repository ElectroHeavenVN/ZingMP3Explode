using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Artists
{
    /// <summary>
    /// Basic information about an artist.
    /// </summary>
    public class IncompleteArtist : IZingMP3Object, ISearchable
    {
        /// <summary>
        /// ID of the artist.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the artist.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Link to the artist's page.
        /// </summary>
        [JsonPropertyName("link")]
        public string Link { get; set; }

        /// <summary>
        /// Indicate whether the artist is in the spotlight.
        /// </summary>
        [JsonPropertyName("spotlight")]
        public bool Spotlight { get; set; }

        /// <summary>
        /// Alias of the artist.
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Link to the small thumbnail of the artist.
        /// </summary>
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        /// Link to the big thumbnail of the artist.
        /// </summary>
        [JsonPropertyName("thumbnailM")]
        public string ThumbnailM { get; set; }

        /// <summary>
        /// Indicate whether the artist is an official account.
        /// </summary>
        [JsonPropertyName("isOA")]
        public bool IsOA { get; set; }

        /// <summary>
        /// Indicate whether the artist is an official account brand.
        /// </summary>
        [JsonPropertyName("isOABrand")]
        public bool IsOABrand { get; set; }

        /// <summary>
        /// Playlist ID of the artist.
        /// </summary>
        [JsonPropertyName("playlistId")]
        public string PlaylistId { get; set; }

        /// <summary>
        /// Number of followers of the artist.
        /// </summary>
        [JsonPropertyName("totalFollow")]
        public long TotalFollow { get; set; }
    }
}
