using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about an artist.</para>
    /// <para xml:lang="vi">Thông tin về một nghệ sĩ.</para>
    /// </summary>
    public class Artist : IncompleteArtist
    {
        [JsonConstructor]
        internal Artist() { }

        /// <summary>
        /// <para xml:lang="en">Url to the cover image of the artist.</para>
        /// <para xml:lang="vi">Liên kết đến ảnh bìa của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("cover")]
        public string Cover { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Biography of the artist.</para>
        /// <para xml:lang="vi">Tiểu sử của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("biography")]
        public string Biography { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Short biography of the artist.</para>
        /// <para xml:lang="vi">Tiểu sử ngắn của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("sortBiography")] // Typo in the API
        public string ShortBiography { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Nationality of the artist.</para>
        /// <para xml:lang="vi">Quốc tịch của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("national")]
        public string Nationality { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Birthday of the artist.</para>
        /// <para xml:lang="vi">Ngày sinh của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("birthday")]
        public string Birthday { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Real name of the artist.</para>
        /// <para xml:lang="vi">Tên thật của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("realname")]
        public string RealName { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">The top album of the artist, displayed as the newest album in the artist's page.</para>
        /// <para xml:lang="vi">Album nổi bật của nghệ sĩ, hiển thị là album mới nhất trên trang nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("topAlbum")]
        public Album? TopAlbum { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Official Account link of the artist.</para>
        /// <para xml:lang="vi">Liên kết tài khoản chính thức của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("oalink")]
        public string OAUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Official Account ID of the artist.</para>
        /// <para xml:lang="vi">ID tài khoản chính thức của nghệ sĩ.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("oaid")]
        public int OAID { get; internal set; }

        /// <inheritdoc cref="IncompleteArtist.IsOA"/>
        [JsonInclude, JsonPropertyName("hasOA")]
        public bool HasOA { get; internal set; }

        /// <inheritdoc cref="IncompleteArtist.TotalFollow"/>
        [JsonInclude, JsonPropertyName("follow")]
        public long Follows { get; internal set; }

        [JsonInclude, JsonPropertyName("awards")]
        internal List<string> awards { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of awards the artist has received.</para>
        /// <para xml:lang="vi">Danh sách giải thưởng mà nghệ sĩ đã nhận được.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> Awards => awards.AsReadOnly();

        [JsonIgnore]
        internal List<PageSection<IZingMP3Object>> sections { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of sections in the artist's page.</para>
        /// <para xml:lang="vi">Danh sách các phân vùng trên trang nghệ sĩ.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<PageSection<IZingMP3Object>> Sections => sections.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Section ID.</para>
        /// <para xml:lang="vi">ID của phân vùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("sectionId")]
        public string SectionID { get; internal set; } = "";

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("tabs")]
        public int[] Tabs { get; internal set; } = [];

        /// <summary>
        /// <para xml:lang="en">Get all singles and EPs of the artist.</para>
        /// <para xml:lang="vi">Lấy tất cả đĩa đơn và đĩa mở rộng của nghệ sĩ.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">A list of singles and EPs.</para>
        /// <para xml:lang="vi">Danh sách các đĩa đơn và đĩa mở rộng.</para>
        /// </returns>
        public List<Album> GetSingleAndEPs()
        {
            List<Album> albums = [];
            foreach (var section in Sections)
                if (section.SectionType == "aSingle")
                    albums.AddRange(section.Items.Cast<Album>());
            return albums;
        }

        /// <summary>
        /// <para xml:lang="en">Get all albums of the artist.</para>
        /// <para xml:lang="vi">Lấy tất cả album của nghệ sĩ.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">A list of albums.</para>
        /// <para xml:lang="vi">Danh sách các album.</para>
        /// </returns>
        public List<Album> GetAlbums()
        {
            List<Album> albums = [];
            foreach (var section in Sections)
                if (section.SectionType == "aAlbum")
                    albums.AddRange(section.Items.Cast<Album>());
            return albums;
        }

        /// <summary>
        /// <para xml:lang="en">Get all albums, singles, and EPs of the artist.</para>
        /// <para xml:lang="vi">Lấy tất cả album, đĩa đơn và đĩa mở rộng của nghệ sĩ.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">A list of albums, singles, and EPs.</para>
        /// <para xml:lang="vi">Danh sách các album, đĩa đơn và đĩa mở rộng.</para>
        /// </returns>
        public List<Album> GetAllAlbums() => GetSingleAndEPs().Concat(GetAlbums()).ToList();

        /// <summary>
        /// <para xml:lang="en">Get popular songs of the artist.</para>
        /// <para xml:lang="vi">Lấy các bài hát nổi bật của nghệ sĩ.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">A list of popular songs.</para>
        /// <para xml:lang="vi">Danh sách các bài hát nổi bật.</para>
        /// </returns>
        public List<Song> GetPopularSongs()
        {
            List<Song> songs = [];
            foreach (var section in Sections)
                if (section.SectionType == "aSongs")
                    songs.AddRange(section.Items.Cast<Song>());
            return songs;
        }

        /// <summary>
        /// <para xml:lang="en">Get music videos (MVs) of the artist.</para>
        /// <para xml:lang="vi">Lấy các video âm nhạc (MV) của nghệ sĩ.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">A list of music videos.</para>
        /// <para xml:lang="vi">Danh sách các video âm nhạc.</para>
        /// </returns>
        public List<IncompleteVideo> GetMVs()
        {
            List<IncompleteVideo> videos = [];
            foreach (var section in Sections)
                if (section.SectionType == "aMV")
                    videos.AddRange(section.Items.Cast<IncompleteVideo>());
            return videos;
        }

        /// <summary>
        /// <para xml:lang="en">Get playlists of the artist.</para>
        /// <para xml:lang="vi">Lấy các playlist của nghệ sĩ.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">A list of playlists.</para>
        /// <para xml:lang="vi">Danh sách các playlist.</para>
        /// </returns>
        public List<Playlist> GetPlaylists()
        {
            List<Playlist> playlists = [];
            foreach (var section in Sections)
                if (section.SectionType == "aPlaylist")
                    playlists.AddRange(section.Items.Cast<Playlist>());
            return playlists;
        }

        /// <summary>
        /// <para xml:lang="en">Get related artists.</para>
        /// <para xml:lang="vi">Lấy các nghệ sĩ liên quan.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">A list of related artists.</para>
        /// <para xml:lang="vi">Danh sách các nghệ sĩ liên quan.</para>
        /// </returns>
        public List<IncompleteArtist> GetRelatedArtists()
        {
            List<IncompleteArtist> artists = [];
            foreach (var section in Sections)
                if (section.SectionType == "aReArtist")
                    artists.AddRange(section.Items.Cast<IncompleteArtist>());
            return artists;
        }
    }
}
