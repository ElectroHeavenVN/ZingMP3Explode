using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Base class for <see cref="Album"/> and <see cref="Playlist"/> containing common properties.</para>
    /// <para xml:lang="vi">Lớp cơ sở cho <see cref="Album"/> và <see cref="Playlist"/> chứa các thuộc tính chung.</para>
    /// </summary>
    public class BaseList : IZingMP3Object, ISearchable
    {
        [JsonConstructor]
        internal BaseList() { } 

        /// <inheritdoc/>
        [JsonInclude, JsonPropertyName("encodeId")]
        public string ID { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Title of the list.</para>
        /// <para xml:lang="vi">Tiêu đề của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("title")]
        public string Title { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">URL of the big thumbnail of the list.</para>
        /// <para xml:lang="vi">URL của ảnh thu nhỏ lớn của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("thumbnailM")]
        public string BigThumbnailUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">URL of the small thumbnail of the list.</para>
        /// <para xml:lang="vi">URL của ảnh thu nhỏ nhỏ của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("thumbnail")]
        public string ThumbnailUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Indicates if this list is official.</para>
        /// <para xml:lang="vi">Danh sách phát này có phải là chính thức hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isoffical")]
        public bool IsOfficial { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">URL of the list.</para>
        /// <para xml:lang="vi">URL của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("link")]
        public string Url { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Indicates if this list is indie.</para>
        /// <para xml:lang="vi">Danh sách phát này có phải là indie hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isIndie")]
        public bool IsIndie { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The release date of the list.</para>
        /// <para xml:lang="vi">Ngày phát hành của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("releaseDate")]
        public string ReleaseDate { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Short description of the list.</para>
        /// <para xml:lang="vi">Mô tả ngắn gọn về danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("sortDescription")] // This is a typo in the API.
        public string ShortDescription { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Description of the list.</para>
        /// <para xml:lang="vi">Mô tả về danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("description")]
        public string Description { get; internal set; } = "";

        [JsonInclude, JsonPropertyName("releasedAt")]
        internal long releaseDateUnix;
        /// <summary>
        /// <para xml:lang="en">The release date of the list in UTC.</para>
        /// <para xml:lang="vi">Ngày phát hành của danh sách theo UTC.</para>
        /// </summary>
        public DateTime ReleaseDateUTC => DateTimeOffset.FromUnixTimeSeconds(releaseDateUnix).UtcDateTime;

        [JsonInclude, JsonPropertyName("genreIds")]
        internal List<string> genreIDs { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">Genre IDs of the list.</para>
        /// <para xml:lang="vi">ID thể loại của danh sách.</para>
        /// </summary>
        public ReadOnlyCollection<string> GenreIDs => genreIDs.AsReadOnly();

        [JsonInclude, JsonPropertyName("artists")]
        internal List<IncompleteArtist> artists { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of artists associated with the list.</para>
        /// <para xml:lang="vi">Danh sách nghệ sĩ liên quan đến danh sách.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<IncompleteArtist> Artists => artists.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">All artists' names separated by commas.</para>
        /// <para xml:lang="vi">Tên tất cả nghệ sĩ, phân tách bằng dấu phẩy.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("artistsNames")]
        public string AllArtistsNames { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">UID of the list.</para>
        /// <para xml:lang="vi">UID của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("uid")]
        public int Uid { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Indicates if this list is private.</para>
        /// <para xml:lang="vi">Danh sách này có riêng tư hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isPrivate")]
        public bool IsPrivate { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Username of the uploader if the song is uploaded by a user.</para>
        /// <para xml:lang="vi">Tên người dùng của người tải lên nếu bài hát được tải lên bởi người dùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("userName")]
        public string Username { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Indicates if this list is an album.</para>
        /// <para xml:lang="vi">Danh sách này có phải là album hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isAlbum")]
        public bool IsAlbum { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The type of the list.</para>
        /// <para xml:lang="vi">Loại của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("textType")]
        public string? Type { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Indicates if this list is a single.</para>
        /// <para xml:lang="vi">Danh sách này có phải là single hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isSingle")]
        public bool? IsSingle { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Name of the list's distributor.</para>
        /// <para xml:lang="vi">Tên nhà phân phối của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("distributor")]
        public string Distributor { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Alias of the list (the string before the ID in the <see cref="Url"/>).</para>
        /// <para xml:lang="vi">Bí danh của danh sách (chuỗi trước ID trong <see cref="Url"/>).</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("aliasTitle")]
        public string Alias { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">The last update timestamp of the list's content.</para>
        /// <para xml:lang="vi">Dấu thời gian cập nhật nội dung cuối cùng của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("contentLastUpdate")]
        public long? ContentLastUpdate { get; internal set; }

        [JsonInclude, JsonPropertyName("genres")]
        internal List<Genre> genres { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">Genres of the list.</para>
        /// <para xml:lang="vi">Các thể loại của danh sách.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<Genre> Genres => genres.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">List of songs in the list.</para>
        /// <para xml:lang="vi">Danh sách bài hát trong danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("song")]
        public SongList Songs { get; internal set; } = new SongList();

        /// <summary>
        /// <para xml:lang="en">Number of likes of the list.</para>
        /// <para xml:lang="vi">Số lượt thích của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("like")]
        public long? Likes { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Number of listens of the list.</para>
        /// <para xml:lang="vi">Số lượt nghe của danh sách.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("listen")]
        public long? Listens { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Indicates if the list is liked by the current user.</para>
        /// <para xml:lang="vi">Danh sách có được người dùng hiện tại thích hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("liked")]
        public bool Liked { get; internal set; }

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("playItemMode")]
        public int PlayItemMode { get; internal set; }

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("subType")]
        public int SubType { get; internal set; }

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("isShuffle")]
        public bool IsShuffle { get; internal set; }

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("sectionId")]
        public string? SectionID { get; internal set; }

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("PR")]
        public bool IsPR { get; internal set; }
    }
}
