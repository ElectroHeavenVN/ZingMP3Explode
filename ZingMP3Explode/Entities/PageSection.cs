using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">A section on a page, containing a list of items.</para>
    /// <para xml:lang="vi">Một phân vùng trên trang, chứa danh sách các đối tượng.</para>
    /// </summary>
    /// <typeparam name="T">
    /// <para xml:lang="en">The type of items in the section. Must implement <see cref="IZingMP3Object"/>.</para>
    /// <para xml:lang="vi">Loại đối tượng trong phân vùng. Phải triển khai <see cref="IZingMP3Object"/>.</para>
    /// </typeparam>
    public class PageSection<T> where T : IZingMP3Object
    {
        [JsonConstructor]
        internal PageSection() { }

        /// <summary>
        /// <para xml:lang="en">Type of the section.</para>
        /// <para xml:lang="vi">Loại của phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("sectionType")]
        public string SectionType { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">View type of the section.</para>
        /// <para xml:lang="vi">Kiểu hiển thị của phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("viewType")]
        public string ViewType { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Title of the section.</para>
        /// <para xml:lang="vi">Tiêu đề của phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("title")]
        public string Title { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">URL of the section.</para>
        /// <para xml:lang="vi">Đường dẫn của phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("link")]
        public string Url { get; internal set; } = "";

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

        /// <summary>
        /// <para xml:lang="en">Top album in the section.</para>
        /// <para xml:lang="vi">Album nổi bật trong phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("topAlbum")]
        public Album? TopAlbum { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Options for the section.</para>
        /// <para xml:lang="vi">Tùy chọn cho phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("options")]
        public PageSectionOption? Options { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Type of items in the section.</para>
        /// <para xml:lang="vi">Loại đối tượng trong phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("itemType")]
        public string? ItemType { get; internal set; }

        internal PageSection<IZingMP3Object> Clone()
        {
            return new PageSection<IZingMP3Object>
            {
                SectionType = SectionType,
                ViewType = ViewType,
                Title = Title,
                Url = Url,
                SectionID = SectionID,
                items = [.. items],
                TopAlbum = TopAlbum,
                Options = Options,
                ItemType = ItemType
            };
        }
    }
}
