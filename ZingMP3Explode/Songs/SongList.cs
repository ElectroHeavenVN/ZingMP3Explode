using System.Text.Json.Serialization;
using ZingMP3Explode.List;

namespace ZingMP3Explode.Songs
{
    /// <summary>
    /// A list of songs.
    /// </summary>
    public class SongList : BaseList<Song>
    {
        /// <summary>
        /// The total duration of all songs in seconds.
        /// </summary>
        [JsonPropertyName("totalDuration")]
        public long? TotalDuration { get; set; }
    }
}
