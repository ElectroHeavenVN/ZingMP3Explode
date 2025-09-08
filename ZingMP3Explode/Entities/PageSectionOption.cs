using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Options for a page section.</para>
    /// <para xml:lang="vi">Tùy chọn cho một phân vùng trang.</para>
    /// </summary>
    public class PageSectionOption
    {
        [JsonConstructor]
        internal PageSectionOption() { }

        /// <summary>
        /// <para xml:lang="en">ID of the artist associated with the section.</para>
        /// <para xml:lang="vi">ID của nghệ sĩ liên quan đến phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("artistId")]
        public string ArtistID { get; internal set; } = "";
    }
}
