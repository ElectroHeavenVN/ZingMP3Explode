using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Clients;
using ZingMP3Explode.Entities.Genres;
using ZingMP3Explode.Entities.Songs;
using ZingMP3Explode.Entities.Videos;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Net;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode
{
    /// <summary>
    /// <para xml:lang="en">Client for interacting with ZingMP3.</para>
    /// <para xml:lang="vi">Client để tương tác với ZingMP3.</para>
    /// </summary>
    public class ZingMP3Client
    {
        /// <summary>
        /// <para xml:lang="en">The API client for making requests to ZingMP3 API.</para>
        /// <para xml:lang="vi">Client API để thực hiện các yêu cầu đến API của ZingMP3.</para>
        /// </summary>
        public ZingMP3APIClient APIClient { get; }

        /// <summary>
        /// <para xml:lang="en">The API key used for authentication.</para>
        /// <para xml:lang="vi">Khóa API được sử dụng để xác thực.</para>
        /// </summary>
        public string APIKey { get; set; } = Constants.DEFAULT_API_KEY;

        /// <summary>
        /// <para xml:lang="en">The secret key used for signing requests.</para>
        /// <para xml:lang="vi">Khóa bí mật được sử dụng để ký các yêu cầu.</para>
        /// </summary>
        public string Secret { get; set; } = Constants.DEFAULT_SECRET;

        /// <summary>
        /// <para xml:lang="en">The version of the ZingMP3 API.</para>
        /// <para xml:lang="vi">Phiên bản của API ZingMP3.</para>
        /// </summary>
        public string Version { get; set; } = Constants.DEFAULT_VERSION;

        internal HttpClient HttpClient { get; }

        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ZingMP3Client"/>.</para>
        /// <para xml:lang="vi">Khởi tạo một đối tượng <see cref="ZingMP3Client"/> mới.</para>
        /// </summary>
        /// <param name="httpClient">
        /// <para xml:lang="en">The <see cref="HttpClient"/> to be used for making requests.</para>
        /// <para xml:lang="vi">Đối tượng <see cref="HttpClient"/> được sử dụng để thực hiện các yêu cầu.</para>
        /// </param>
        /// <param name="apiKey">
        /// <para xml:lang="en">The API key used for authentication.</para>
        /// <para xml:lang="vi">Khóa API được sử dụng để xác thực.</para>
        /// </param>
        /// <param name="secret">
        /// <para xml:lang="en">The secret key used for signing requests.</para>
        /// <para xml:lang="vi">Khóa bí mật được sử dụng để ký các yêu cầu.</para>
        /// </param>
        /// <param name="version">
        /// <para xml:lang="en">The version of the ZingMP3 API.</para>
        /// <para xml:lang="vi">Phiên bản của API ZingMP3.</para>
        /// </param>
        public ZingMP3Client(HttpClient httpClient, string apiKey = Constants.DEFAULT_API_KEY, string secret = Constants.DEFAULT_SECRET, string version = Constants.DEFAULT_VERSION)
        {
            if (!httpClient.DefaultRequestHeaders.Contains("User-Agent"))
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Utils.RandomUserAgent());
            HttpClient = httpClient;
            APIKey = apiKey;
            Secret = secret;
            Version = version;
            APIClient = new ZingMP3APIClient(this);
            Songs = new SongClient(this);
            Artists = new ArtistClient(this);
            Albums = new AlbumClient(this);
            Playlists = new PlaylistClient(this);
            Videos = new VideoClient(this);
            Genres = new GenreClient(this);
            Search = new SearchClient(this);
            CurrentUser = new UserClient(this);
        }

        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ZingMP3Client"/>.</para>
        /// <para xml:lang="vi">Khởi tạo một đối tượng <see cref="ZingMP3Client"/> mới.</para>
        /// </summary>
        /// <param name="apiKey">
        /// <para xml:lang="en">The API key used for authentication.</para>
        /// <para xml:lang="vi">Khóa API được sử dụng để xác thực.</para>
        /// </param>
        /// <param name="secret">
        /// <para xml:lang="en">The secret key used for signing requests.</para>
        /// <para xml:lang="vi">Khóa bí mật được sử dụng để ký các yêu cầu.</para>
        /// </param>
        /// <param name="version">
        /// <para xml:lang="en">The version of the ZingMP3 API.</para>
        /// <para xml:lang="vi">Phiên bản của API ZingMP3.</para>
        /// </param>
        public ZingMP3Client(string apiKey = Constants.DEFAULT_API_KEY, string secret = Constants.DEFAULT_SECRET, string version = Constants.DEFAULT_VERSION) : this(new HttpClient(new HttpClientHandler()
        {
#if NETFRAMEWORK
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
#else
                AutomaticDecompression = DecompressionMethods.All,
#endif
        }), apiKey, secret, version)
        { }

        /// <summary>
        /// <para xml:lang="en">Client for song-related operations.</para>
        /// <para xml:lang="vi">Client để thực hiện các thao tác liên quan đến bài hát.</para>
        /// </summary>
        public SongClient Songs { get; }

        /// <summary>
        /// <para xml:lang="en">Client for artist-related operations.</para>
        /// <para xml:lang="vi">Client để thực hiện các thao tác liên quan đến nghệ sĩ.</para>
        /// </summary>
        public ArtistClient Artists { get; }

        /// <summary>
        /// <para xml:lang="en">Client for album-related operations.</para>
        /// <para xml:lang="vi">Client để thực hiện các thao tác liên quan đến album.</para>
        /// </summary>
        public AlbumClient Albums { get; }

        /// <summary>
        /// <para xml:lang="en">Client for playlist-related operations.</para>
        /// <para xml:lang="vi">Client để thực hiện các thao tác liên quan đến danh sách phát.</para>
        /// </summary>
        public PlaylistClient Playlists { get; }

        /// <summary>
        /// <para xml:lang="en">Client for video-related operations.</para>
        /// <para xml:lang="vi">Client để thực hiện các thao tác liên quan đến video.</para>
        /// </summary>
        public VideoClient Videos { get; }

        /// <summary>
        /// <para xml:lang="en">Client for genre-related operations.</para>
        /// <para xml:lang="vi">Client để thực hiện các thao tác liên quan đến thể loại.</para>
        /// </summary>
        public GenreClient Genres { get; }

        /// <summary>
        /// <para xml:lang="en">Client for search-related operations.</para>
        /// <para xml:lang="vi">Client để thực hiện các thao tác liên quan đến tìm kiếm.</para>
        /// </summary>
        public SearchClient Search { get; }

        /// <summary>
        /// <para xml:lang="en">Client for current user-related operations.</para>
        /// <para xml:lang="vi">Client để thực hiện các thao tác liên quan đến người dùng hiện tại.</para>
        /// </summary>
        public UserClient CurrentUser { get; }

        /// <summary>
        /// <para xml:lang="en">Initializes the client by fetching the latest API key, secret, and version from ZingMP3.</para>
        /// <para xml:lang="vi">Khởi tạo client bằng cách lấy khóa API, khóa bí mật và phiên bản mới nhất từ ZingMP3.</para>
        /// </summary>
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var httpResponse = await HttpClient.GetAsync(Constants.ZINGMP3_LINK, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new ZingMP3ExplodeException($"Failed to fetch ZingMP3 main page. Status code: {httpResponse.StatusCode}");
#if NETFRAMEWORK
            string html = await httpResponse.Content.ReadAsStringAsync();
#else
            string html = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
#endif
            Match match = Regexes.MainMinJS.Match(html);
            Version = match.Groups[1].Value;
            string mainMinJSUrl = match.Value;
            var handler = new HttpClientHandler()
            {
#if NETFRAMEWORK
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
#else
                AutomaticDecompression = DecompressionMethods.All,
#endif
            };
            HttpClient client = new HttpClient(handler, true);
            client.DefaultRequestHeaders.Referrer = new Uri(Constants.ZINGMP3_LINK);
            httpResponse = await client.GetAsync(mainMinJSUrl, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new ZingMP3ExplodeException($"Failed to fetch main.min.js. Status code: {httpResponse.StatusCode}");
#if NETFRAMEWORK
            string mainMinJS = await httpResponse.Content.ReadAsStringAsync();
#else
            string mainMinJS = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
#endif
            int startIndex = mainMinJS.IndexOf("\"NON_LOGGED_ADD_RECENT_PLAYLIST\"");
            mainMinJS = mainMinJS.Substring(startIndex, mainMinJS.IndexOf("\"STORAGE_ADD_SONG\"") - startIndex);
            Match apiKeyAndSecretMatch = Regexes.ApiKeySecret.Match(mainMinJS);
            APIKey = apiKeyAndSecretMatch.Groups[1].Value;
            Secret = apiKeyAndSecretMatch.Groups[2].Value;
        }
    }
}
