using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Genres
{
    /// <summary>
    /// Basic information about a genre.
    /// </summary>
    public class IncompleteGenre : IZingMP3Object
    {
        /// <summary>
        /// The genre ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the genre.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Title of the genre.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Alias of the genre.
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Link to the genre.
        /// </summary>
        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
