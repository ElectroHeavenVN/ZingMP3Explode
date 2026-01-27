namespace ZingMP3Explode.Utilities
{
    internal static class Constants
    {
        //latest version available at: https://github.com/zmp3-pc/zmp3-pc/releases
        //WHY do they host the binary on GitHub when they clearly can afford their own CDN?
        //Also, zmp3-pc is a personal account, not an organization.
        internal const string ZING_MP3_DESKTOP_CLIENT_UA = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) zmp3/1.2.0 Chrome/102.0.5005.167 Electron/19.1.9 Safari/537.36";

        internal const string DEFAULT_API_KEY = "X5BM3w8N7MKozC0B85o4KMlzLZKhV00y";
        internal const string DEFAULT_SECRET = "acOrvUS15XRW2o9JksiK1KgQ6Vbds8ZW";
        internal const string DEFAULT_VERSION = "1.17.6";

        internal static readonly string API_BASE_PATH = "/api/v2/";
        internal static readonly string ZINGMP3_LINK = "https://zingmp3.vn/";
        internal static readonly string[] HASH_PARAMS = ["ctime", "id", "type", "page", "count", "version"];
    }
}
