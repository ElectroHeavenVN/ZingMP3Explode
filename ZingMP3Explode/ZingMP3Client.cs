using System.Net;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Bridge;
using ZingMP3Explode.Search;
using ZingMP3Explode.Songs;
using ZingMP3Explode.Artists;
using ZingMP3Explode.Albums;
using ZingMP3Explode.Genres;
using ZingMP3Explode.Playlists;
using ZingMP3Explode.Videos;
using System.Text.Json.Nodes;
using ZingMP3Explode.Authentication;
using ZingMP3Explode.Utilities;
using System.Text.Json;
using ZingMP3Explode.Exceptions;
using System.Collections.Generic;
using ZingMP3Explode.Cookies;

namespace ZingMP3Explode
{
    public class ZingMP3Client
    {
        HttpClient httpClient;
        ZingMP3Endpoint endpoint;
        static readonly Regex mainMinJSRegex = new Regex("(https://zjs.zmdcdn.me/zmp3-desktop/releases/.*?/static/js/main.min.js)", RegexOptions.Compiled);
        static readonly Regex apiKeyAndSecretRegex = new Regex(";var .=\"(.{32})\",.=\"(.{32})\",.=\\{publicKey:", RegexOptions.Compiled);
        static readonly Regex zaloVersionRegex = new Regex("\"clientVersion\":\"([0-9.]*?)\"", RegexOptions.Compiled);
        string zaloVersion = Constants.DEFAULT_ZALO_VER;
        bool isZaloVersionUpdated;
        CookieContainer? cookieContainer;

        public string APIKey { get; set; } = Constants.DEFAULT_API_KEY;

        public string Secret { get; set; } = Constants.DEFAULT_SECRET;

        public ZingMP3Client(HttpClient httpClient, string apiKey, string secret) => Ctor(httpClient, apiKey, secret);

        public ZingMP3Client(string apiKey, string secret, List<ZingMP3Cookie> cookies) => Ctor(Http.GetClientWithCookie(cookies, out cookieContainer), apiKey, secret);

        public ZingMP3Client(string apiKey, string secret) => Ctor(Http.GetClient(out cookieContainer), apiKey, secret);

        public ZingMP3Client(HttpClient httpClient) => Ctor(httpClient);

        public ZingMP3Client(List<ZingMP3Cookie> cookies) => Ctor(Http.GetClientWithCookie(cookies, out cookieContainer));

        public ZingMP3Client() => Ctor(Http.GetClient(out cookieContainer));

        void Ctor(HttpClient client)
        {
            if (!client.DefaultRequestHeaders.Contains("User-Agent"))
                client.DefaultRequestHeaders.UserAgent.ParseAdd(Http.RandomUserAgent());
            httpClient = client;
            endpoint = new ZingMP3Endpoint(httpClient);
            InitializeClients();
        }

        void Ctor(HttpClient client, string apiKey, string secret)
        {
            if (!client.DefaultRequestHeaders.Contains("User-Agent"))
                client.DefaultRequestHeaders.UserAgent.ParseAdd(Http.RandomUserAgent());
            httpClient = client;
            APIKey = apiKey;
            Secret = secret;
            endpoint = new ZingMP3Endpoint(httpClient)
            {
                APIKey = apiKey,
                Secret = secret
            };
            InitializeClients();
        }

        public SongClient Songs { get; private set; }

        public ArtistClient Artists { get; private set; }

        public AlbumClient Albums { get; private set; }

        public PlaylistClient Playlists { get; private set; }

        public VideoClient Videos { get; private set; }

        public GenreClient Genres { get; private set; }

        public SearchClient Search { get; private set; }

        void InitializeClients()
        {
            Songs = new SongClient(endpoint);
            Artists = new ArtistClient(endpoint);
            Albums = new AlbumClient(endpoint);
            Playlists = new PlaylistClient(endpoint);
            Videos = new VideoClient(endpoint);
            Genres = new GenreClient(endpoint);
            Search = new SearchClient(endpoint);
        }

        /// <summary>
        /// Initializes the client by fetching the API key and secret from the ZingMP3 website.
        /// </summary>
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var httpResponse = await httpClient.GetAsync(Constants.ZINGMP3_LINK, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to fetch ZingMP3 main page. Status code: {httpResponse.StatusCode}");
            string html = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            string mainMinJSUrl = mainMinJSRegex.Match(html).Groups[1].Value;
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };
            HttpClient client = new HttpClient(handler, true);
            client.DefaultRequestHeaders.Referrer = new Uri(Constants.ZINGMP3_LINK);
            httpResponse = await client.GetAsync(mainMinJSUrl, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to fetch main.min.js. Status code: {httpResponse.StatusCode}");
            string mainMinJS = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            int startIndex = mainMinJS.IndexOf("\"NON_LOGGED_ADD_RECENT_PLAYLIST\"");
            mainMinJS = mainMinJS.Substring(startIndex, mainMinJS.IndexOf("\"STORAGE_ADD_SONG\"") - startIndex);
            Match apiKeyAndSecretMatch = apiKeyAndSecretRegex.Match(mainMinJS);
            APIKey = apiKeyAndSecretMatch.Groups[1].Value;
            Secret = apiKeyAndSecretMatch.Groups[2].Value;
            endpoint.APIKey = APIKey;
            endpoint.Secret = Secret;
        }

        async Task UpdateZaloVersionAsync(CancellationToken cancellationToken = default)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Constants.ID_ZALO_LINK.TrimEnd('/') + "/account?continue=" + Uri.EscapeDataString(Constants.ZINGMP3_LINK + "?isZaloPopupLogin=1"), cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to fetch Zalo login site. Status code: {httpResponse.StatusCode}");
            if (isZaloVersionUpdated)
                return;
            string html = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            int startIndex = html.IndexOf("\"clientVersion\"");
            html = html.Substring(startIndex, html.IndexOf("www.google-analytics.com") - startIndex);
            string zaloVersion = zaloVersionRegex.Match(html).Groups[1].Value;
            this.zaloVersion = zaloVersion;
            isZaloVersionUpdated = true;
        }

        public async Task LoginWithQRCodeAsync(Action<QRCodeAuthentication> beforeScanQRCodeCallback, Action<QRCodeAuthenticationUser> afterScanQRCodeCallback, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage GetHttpRequestMessage(string endpoint, IEnumerable<KeyValuePair<string, string?>> nameValueCollection)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(Constants.ID_ZALO_LINK.TrimEnd('/') + endpoint));
                request.Content = new FormUrlEncodedContent(nameValueCollection);
                request.Headers.Add("Referer", Constants.ID_ZALO_LINK.TrimEnd('/') + "/account?continue=" + Uri.EscapeDataString(Constants.ZINGMP3_LINK + "?isZaloPopupLogin=1"));
                return request;
            }

            await UpdateZaloVersionAsync(cancellationToken);
            HttpRequestMessage request = GetHttpRequestMessage("/account/logininfo", new Dictionary<string, string?>
            {
                { "continue", Constants.ZINGMP3_LINK + "?isZaloPopupLogin=1" },
                { "v", zaloVersion }
            });
            HttpResponseMessage httpResponse = await httpClient.SendAsync(request, cancellationToken);
            string resolvedJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            request = GetHttpRequestMessage("/account/verify-client", new Dictionary<string, string?>
            {
                { "type", "device" },
                { "continue", Constants.ZINGMP3_LINK + "?isZaloPopupLogin=1" },
                { "v", zaloVersion }
            });
            httpResponse = await httpClient.SendAsync(request, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new ZingMP3ExplodeException($"Failed to verify client. Status code: {httpResponse.StatusCode}");
            resolvedJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            CheckErrorCode(resolvedJson, out _);
            request = GetHttpRequestMessage("/account/authen/qr/generate", new Dictionary<string, string?>
            {
                { "continue", Constants.ZINGMP3_LINK + "?isZaloPopupLogin=1" },
                { "v", zaloVersion }
            });
            httpResponse = await httpClient.SendAsync(request, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new ZingMP3ExplodeException($"Failed to fetch QR code. Status code: {httpResponse.StatusCode}");
            resolvedJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            CheckErrorCode(resolvedJson, out JsonNode node);
            QRCodeAuthentication? auth = node.Deserialize<QRCodeAuthentication>(JsonDefaults.Options);
            if (auth == null)
                throw new JsonException("Failed to deserialize QR code authentication response.");
            beforeScanQRCodeCallback(auth);
            request = GetHttpRequestMessage("/account/authen/qr/waiting-scan", new Dictionary<string, string?>
            {
                { "code", auth?.Code ?? "" },
                { "continue", Constants.ZINGMP3_LINK + "?isZaloPopupLogin=1" },
                { "v", zaloVersion }
            });
            httpResponse = await httpClient.SendAsync(request, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new ZingMP3ExplodeException($"Failed to wait for user to scan the QR code. Status code: {httpResponse.StatusCode}");
            resolvedJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            CheckErrorCode(resolvedJson, out node);
            QRCodeAuthenticationUser? user = node.Deserialize<QRCodeAuthenticationUser>(JsonDefaults.Options);
            if (user == null)
                throw new JsonException("Failed to deserialize QR code authentication user response.");
            afterScanQRCodeCallback(user);
            request = GetHttpRequestMessage("/account/authen/qr/waiting-confirm", new Dictionary<string, string?>
            {
                { "code", auth?.Code ?? "" },
                { "gToken", "" },
                { "gAction", "CONFIRM_QR" },
                { "continue", Constants.ZINGMP3_LINK + "?isZaloPopupLogin=1" },
                { "v", zaloVersion }
            });
            httpResponse = await httpClient.SendAsync(request, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new ZingMP3ExplodeException($"Failed to wait for user confirmation. Status code: {httpResponse.StatusCode}");
            resolvedJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            CheckErrorCode(resolvedJson, out _);
            httpResponse = await httpClient.GetAsync(Constants.ID_ZALO_LINK.TrimEnd('/') + "/account/checksession?continue=" + Uri.EscapeDataString(Constants.ZINGMP3_LINK + "?isZaloPopupLogin=1"), cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new ZingMP3ExplodeException($"Failed to check for the session. Status code: {httpResponse.StatusCode}");
        }

        public string GetCookiesAsString()
        {
            string cookies = "";
            if (cookieContainer == null)
                throw new ZingMP3ExplodeException("The current client was initialized using custom HttpClient object.");
            foreach (Cookie cookie in cookieContainer.GetAllCookies())
                cookies += $"{cookie.Name}={cookie.Value}; ";
            return cookies;
        }

        public List<ZingMP3Cookie> GetCookies()
        {
            List<ZingMP3Cookie> cookies = new List<ZingMP3Cookie>();
            if (cookieContainer == null)
                throw new ZingMP3ExplodeException("The current client was initialized using custom HttpClient object.");
            foreach (Cookie cookie in cookieContainer.GetAllCookies())
                cookies.Add(ZingMP3Cookie.FromCookie(cookie));
            return cookies;
        }

        static void CheckErrorCode(string json, out JsonNode node)
        {
            node = JsonNode.Parse(json);
            int errorCode = node["error_code"].GetValue<int>();
            if (errorCode != 0)
                throw new ZingMP3ExplodeException(errorCode, node["error_message"].GetValue<string>());
            if (!node.AsObject().ContainsKey("data"))
                throw new ZingMP3ExplodeException("The API does not return any data");
            node = node["data"];
        }
    }
}
