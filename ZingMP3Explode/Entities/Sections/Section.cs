using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">A section containing a list of items.</para>
    /// <para xml:lang="vi">Một phân vùng chứa danh sách các đối tượng.</para>
    /// </summary>
    /// <typeparam name="T">
    /// <para xml:lang="en">The type of items in the section. Must implement <see cref="IZingMP3Object"/>.</para>
    /// <para xml:lang="vi">Loại đối tượng trong phân vùng. Phải triển khai <see cref="IZingMP3Object"/>.</para>
    /// </typeparam>
    public class Section<T> where T: IZingMP3Object
    {
        /// <summary>
        /// <para xml:lang="en">Type of the section.</para>
        /// <para xml:lang="vi">Loại của phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("sectionType")]
        public string SectionType { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">ID of the section.</para>
        /// <para xml:lang="vi">ID của phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("sectionId")]
        public string SectionID { get; internal set; } = "";

        [JsonInclude, JsonPropertyName("items")]
        internal List<T> items { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of items in the section.</para>
        /// <para xml:lang="vi">Danh sách các đối tượng trong phân vùng.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<T> Items => items.AsReadOnly();
    }
}
