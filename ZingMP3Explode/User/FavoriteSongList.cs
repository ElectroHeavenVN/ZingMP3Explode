using System.Text.Json.Serialization;
using ZingMP3Explode.List;
using ZingMP3Explode.Songs;

namespace ZingMP3Explode.User
{
    public class FavoriteSongList : BaseList<Song>
    {
        [JsonPropertyName("sectionId")]
        public string? SectionId { get; set; }
    }
}
