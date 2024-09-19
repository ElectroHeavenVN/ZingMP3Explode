using System.Text.Json.Serialization;
using ZingMP3Explode.Albums;
using ZingMP3Explode.List;

namespace ZingMP3Explode.User
{
    public class FavoriteAlbumList : BaseList<Album>
    {
        [JsonPropertyName("sectionId")]
        public string? SectionId { get; set; }
    }
}
