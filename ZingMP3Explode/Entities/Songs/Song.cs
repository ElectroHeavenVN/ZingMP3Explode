using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information of a song.</para>
    /// <para xml:lang="vi">Thông tin về một bài hát.</para>
    /// </summary>
    public class Song : IZingMP3Object, ISearchable
    {
        [JsonConstructor]
        internal Song() { }

        [JsonInclude, JsonPropertyName("releaseDate")]
        internal long releaseDateUnix;

        /// <inheritdoc/>
        /// <summary>
        /// <para xml:lang="en">ID of the song.</para>
        /// <para xml:lang="vi">ID của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("encodeId")]
        public string ID { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">The relative URL of the song.</para>
        /// <para xml:lang="vi">Đường dẫn tương đối của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("link")]
        public string Url { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">The full URL of the song.</para>
        /// <para xml:lang="vi">Đường dẫn đầy đủ của bài hát.</para>
        /// </summary>
        [JsonIgnore]
        public string FullUrl => Constants.ZINGMP3_LINK.TrimEnd('/') + Url;

        /// <summary>
        /// <para xml:lang="en">Short URL of the song. The URL will be redirected to the full URL.</para>
        /// <para xml:lang="vi">Đường dẫn rút gọn của bài hát. Đường dẫn này sẽ chuyển hướng đến đường dẫn đầy đủ.</para>
        /// </summary>
        [JsonIgnore]
        public string ShortUrl => $"https://zingmp3.vn/bai-hat/{ID}.html";

        /// <inheritdoc />
        /// <summary>
        /// <para xml:lang="en">Title of the song.</para>
        /// <para xml:lang="vi">Tiêu đề của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("title")]
        public string Title { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Alias of the song (the string before the ID in the <see cref="Url"/>).</para>
        /// <para xml:lang="vi">Bí danh của bài hát (chuỗi trước ID trong <see cref="Url"/>).</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("alias")]
        public string Alias { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Whether the song is official.</para>
        /// <para xml:lang="vi">Bài hát có phải là chính thức hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isOffical")] // Typo in the API
        public bool IsOfficial { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Username of the uploader if the song is uploaded by a user.</para>
        /// <para xml:lang="vi">Tên người dùng của người tải lên nếu bài hát được tải lên bởi người dùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("username")]
        public string Username { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">All artists' names separated by commas.</para>
        /// <para xml:lang="vi">Tên tất cả nghệ sĩ, phân tách bằng dấu phẩy.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("artistsNames")]
        public string AllArtistsNames { get; internal set; } = "";

        [JsonInclude, JsonPropertyName("artists")]
        internal List<IncompleteArtist> artists { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of artists associated with the song.</para>
        /// <para xml:lang="vi">Danh sách nghệ sĩ liên quan đến bài hát.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<IncompleteArtist> Artists => artists.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Whether the song is world-wide or the song is only available in Vietnam.</para>
        /// <para xml:lang="vi">Bài hát có phát hành toàn cầu hay chỉ ở Việt Nam.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isWorldWide")]
        public bool IsWorldWide { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Preview of the song if the song is locked behind the paywall.</para>
        /// <para xml:lang="vi">Phần nghe thử của bài hát nếu bài hát yêu cầu trả phí.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("previewInfo")]
        public PreviewInfo? PreviewInfo { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">URL of the big thumbnail of the song.</para>
        /// <para xml:lang="vi">Đường dẫn ảnh thu nhỏ lớn của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("thumbnailM")]
        public string BigThumbnailUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">URL of the small thumbnail of the song.</para>
        /// <para xml:lang="vi">Đường dẫn ảnh thu nhỏ nhỏ của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("thumbnail")]
        public string ThumbnailUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Duration of the song in seconds.</para>
        /// <para xml:lang="vi">Thời lượng của bài hát (tính bằng giây).</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("duration")]
        public long Duration { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Whether the song is a Zing Choice song.</para>
        /// <para xml:lang="vi">Bài hát có phải là Zing Choice hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("zingChoice")]
        public bool IsZingChoice { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Whether the song is private.</para>
        /// <para xml:lang="vi">Bài hát có phải là riêng tư hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isPrivate")]
        public bool IsPrivate { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Whether the song is a pre-release song.</para>
        /// <para xml:lang="vi">Bài hát có phải là phát hành trước hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("preRelease")]
        public bool IsPreRelease { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The release date of the song in UTC.</para>
        /// <para xml:lang="vi">Ngày phát hành của bài hát theo UTC.</para>
        /// </summary>
        public DateTime ReleaseDateUTC => DateTimeOffset.FromUnixTimeSeconds(releaseDateUnix).DateTime;

        [JsonInclude, JsonPropertyName("genreIds")]
        internal List<string> genreIDs { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">Genre IDs of the song.</para>
        /// <para xml:lang="vi">ID thể loại của bài hát.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> GenreIDs => genreIDs.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Album of the song.</para>
        /// <para xml:lang="vi">Album của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("album")]
        public Album? Album { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Name of the song's distributor.</para>
        /// <para xml:lang="vi">Tên nhà phân phối của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("distributor")]
        public string Distributor { get; internal set; } = "";

        [JsonInclude, JsonPropertyName("indicators")]
        internal List<string> indicators { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">List of indicators of the song, for example "explicit".</para>
        /// <para xml:lang="vi">Danh sách các chỉ báo của bài hát, ví dụ "explicit".</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> Indicators => indicators.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Radio ID of the song.</para>
        /// <para xml:lang="vi">ID radio của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("radioId")]
        public long RadioID { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Radio playlist of the song.</para>
        /// <para xml:lang="vi">Danh sách phát radio của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("radio")]
        public Playlist? Radio { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Whether the song is indie.</para>
        /// <para xml:lang="vi">Bài hát có phải là indie hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isIndie")]
        public bool IsIndie { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">URL of the MV of the song.</para>
        /// <para xml:lang="vi">Đường dẫn MV của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("mvlink")]
        public string MVUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">The streaming status of the song.</para>
        /// <para xml:lang="vi">Trạng thái phát trực tuyến của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("streamingStatus")]
        public SubscriptionType StreamingStatus { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The stream privileges of the song.</para>
        /// <para xml:lang="vi">Quyền phát trực tuyến của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("streamPrivileges")]
        public SubscriptionType[] StreamPrivileges { get; internal set; } = [];

        /// <summary>
        /// <para xml:lang="en">The download privileges of the song.</para>
        /// <para xml:lang="vi">Quyền tải về của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("downloadPrivileges")]
        public SubscriptionType[] DownloadPrivileges { get; internal set; } = [];

        /// <summary>
        /// <para xml:lang="en">Indicate if audio ads can be played.</para>
        /// <para xml:lang="vi">Cho phép phát quảng cáo âm thanh hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("allowAudioAds")]
        public bool AllowAudioAds { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Indicate if the song has lyrics.</para>
        /// <para xml:lang="vi">Bài hát có lời hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("hasLyric")]
        public bool HasLyric { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The user ID of the uploader.</para>
        /// <para xml:lang="vi">ID người dùng của người tải lên.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("userid")]
        public long UserID { get; internal set; }

        [JsonInclude, JsonPropertyName("genres")]
        internal List<Genre> genres { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">Genres of the song.</para>
        /// <para xml:lang="vi">Các thể loại của bài hát.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<Genre> Genres => genres.AsReadOnly();

        [JsonInclude, JsonPropertyName("composers")]
        internal List<IncompleteArtist> composers { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">Composers of the song.</para>
        /// <para xml:lang="vi">Nhạc sĩ sáng tác bài hát.</para>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<IncompleteArtist> Composers => composers.AsReadOnly();

        /// <summary>
        /// <para xml:lang="en">Indicate if the song can be registered as a ring back tone.</para>
        /// <para xml:lang="vi">Bài hát có thể đăng ký làm nhạc chờ hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isRBT")]
        public bool IsRBT { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Numbers of likes of the song.</para>
        /// <para xml:lang="vi">Số lượt thích của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("like")]
        public long Likes { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Numbers of listens of the song.</para>
        /// <para xml:lang="vi">Số lượt nghe của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("listen")]
        public long Listens { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Indicate if the song is liked by the current user.</para>
        /// <para xml:lang="vi">Bài hát có được người dùng hiện tại thích hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("liked")]
        public bool Liked { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Number of comments of the song.</para>
        /// <para xml:lang="vi">Số bình luận của bài hát.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("comment")]
        public long Comments { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The ranking status of the song in the #zingchart leaderboard. Negative values indicate the rank of the song has dropped, and vice versa.</para>
        /// <para xml:lang="vi">Trạng thái xếp hạng của bài hát trên bảng xếp hạng #zingchart. Giá trị âm cho biết thứ hạng giảm, ngược lại là tăng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("rakingStatus")]
        public int RankingStatus { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The ranking score of the song in the #zingchart leaderboard.</para>
        /// <para xml:lang="vi">Điểm xếp hạng của bài hát trên bảng xếp hạng #zingchart.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("score")]
        public int RankingScore { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The weekly ranking of the song in the weekly leaderboard.</para>
        /// <para xml:lang="vi">Thứ tự xếp hạng của bài hát trong bảng xếp hạng tuần.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("weeklyRanking")]
        public int WeeklyRanking { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">The main artist of the song.</para>
        /// <para xml:lang="vi">Nghệ sĩ chính của bài hát.</para>
        /// </summary>
        /// <remarks>
        /// <para xml:lang="en">This property is populated when the current song is in the top 3 of the #zingchart leaderboard, and is returned by the chart API.</para>
        /// <para xml:lang="vi">Thuộc tính này được nạp khi bài hát hiện tại nằm trong top 3 của bảng xếp hạng #zingchart, và được trả về bởi API bảng xếp hạng.</para>
        /// </remarks>
        [JsonInclude, JsonPropertyName("artist")]
        public IncompleteArtist? Artist { get; internal set; }

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("publicStatus")]
        public int PublicStatus { get; internal set; }
        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("statusCode")]
        public int StatusCode { get; internal set; }
        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("statusName")]
        public string StatusName { get; internal set; } = "";
        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("uname")]
        public string UName { get; internal set; } = "";
        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("canEdit")]
        public bool CanEdit { get; internal set; }
        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("canDelete")]
        public bool CanDelete { get; internal set; }
        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("totalTopZing")]
        public int TotalTopZing { get; internal set; }
    }
}
