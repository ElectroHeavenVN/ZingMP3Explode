using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZingMP3Explode.Albums;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Sections
{
    /// <summary>
    /// Represents a section in a page.
    /// </summary>
    public class Section
    {
        public class SectionOption
        {
            [JsonPropertyName("artistId")]
            public string ArtistId { get; set; }
        }

        [JsonPropertyName("sectionType")]
        public string SectionType { get; set; }

        [JsonPropertyName("viewType")]
        public string ViewType { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("link")]
        public string Link { get; set; }
        
        [JsonPropertyName("sectionId")]
        public string SectionId { get; set; }
        
        [JsonPropertyName("items")]
        public List<IZingMP3Object> Items { get; set; }
        
        [JsonPropertyName("topAlbum")]
        public Album? TopAlbum { get; set; }
        
        [JsonPropertyName("options")]
        public SectionOption? Options { get; set; }
        
        [JsonPropertyName("itemType")]
        public string? ItemType { get; set; }
    }
}
