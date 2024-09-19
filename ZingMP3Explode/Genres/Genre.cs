using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Genres
{
    /// <summary>
    /// Full information about a genre, returned by <see cref="GenreClient.GetAsync"/>.
    /// </summary>
    //https://zingmp3.vn/api/v2/genre/get/info
    public class Genre : IncompleteGenre
    {
        /// <summary>
        /// Parent genre.
        /// </summary>
        [JsonPropertyName("parent")]
        public IncompleteGenre Parent { get; set; }

        /// <summary>
        /// Child genres.
        /// </summary>
        [JsonPropertyName("childs")]
        public List<IncompleteGenre> Childs { get; set; }
    }
}
