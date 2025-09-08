using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Result of a multi-category search.</para>
    /// <para xml:lang="vi">Kết quả của một tìm kiếm đa loại.</para>
    /// </summary>
    public class MultiSearchResult
    {
        [JsonConstructor]
        internal MultiSearchResult() { }

        [JsonInclude, JsonPropertyName("songs")]
        internal List<Song> songs { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of songs in the search result.</para>
        /// <para xml:lang="vi">Danh sách các bài hát trong kết quả tìm kiếm.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<Song> Songs => songs.AsReadOnly();

        [JsonInclude, JsonPropertyName("artists")]
        internal List<IncompleteArtist> artists { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of artists in the search result.</para>
        /// <para xml:lang="vi">Danh sách các nghệ sĩ trong kết quả tìm kiếm.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<IncompleteArtist> Artists => artists.AsReadOnly();

        [JsonInclude, JsonPropertyName("videos")]
        internal List<IncompleteVideo> videos { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of videos in the search result.</para>
        /// <para xml:lang="vi">Danh sách các video trong kết quả tìm kiếm.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<IncompleteVideo> Videos => videos.AsReadOnly();

        [JsonInclude, JsonPropertyName("playlists")]
        internal List<Playlist> playlists { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of playlists in the search result.</para>
        /// <para xml:lang="vi">Danh sách các danh sách phát trong kết quả tìm kiếm.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<Playlist> Playlists => playlists.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Information about the number of results in each category.</para>
        /// <para xml:lang="vi">Thông tin về số lượng kết quả trong từng loại.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("counter")]
        public MultiSearchResultCounter Counter { get; internal set; } = new MultiSearchResultCounter();

        /// <summary>
        /// <para xml:lang="en">Section ID of the search result.</para>
        /// <para xml:lang="vi">ID phân vùng của kết quả tìm kiếm.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("sectionId")]
        public string SectionID { get; internal set; } = "";
    }
}
