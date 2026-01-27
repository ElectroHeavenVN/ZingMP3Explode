using System.Text.RegularExpressions;

namespace ZingMP3Explode.Utilities
{
    internal static partial class Regexes
    {
        /* language=regex */
        const string MAIN_MIN_JS_REGEX_PATTERN = "https:\\/\\/zmdjs.zmdcdn.me\\/zmp3-desktop\\/v(.*?)\\/static\\/js\\/main\\.min\\.js";
        /* language=regex */
        const string APIKEY_SECRET_REGEX_PATTERN = ";var .=\"(.{32})\",.=\"(.{32})\",.=\\{publicKey:";
        /* language=regex */
        const string ALBUM_PLAYLIST_ID_REGEX_PATTERN = @"^[A-Z0-9]{8}$";
        /* language=regex */
        const string ALBUM_URL_REGEX_PATTERN = @"zingmp3\.vn\/album\/(.*?)\/([A-Z0-9]{8})\.html";
        /* language=regex */
        const string SHORT_ALBUM_URL_REGEX_PATTERN = @"zingmp3\.vn\/album\/(?:default\/)?([A-Z0-9]{8})\.html";
        /* language=regex */
        const string ARTIST_URL_REGEX_PATTERN = @"zingmp3\.vn\/(?:nghe-si\/)?(.*)";
        /* language=regex */
        const string ARTIST_GENRE_WLB_ID_REGEX_PATTERN = @"^I[A-Z0-9]{7}$";
        /* language=regex */
        const string ARTIST_ALIAS_REGEX_PATTERN = @"^[a-zA-Z0-9.-]*$";
        /* language=regex */
        const string PLAYLIST_URL_REGEX_PATTERN = @"zingmp3\.vn\/playlist\/(.*?)\/([A-Z0-9]{8})\.html";
        /* language=regex */
        const string SONG_URL_REGEX_PATTERN = @"zingmp3\.vn\/bai-hat\/(.*?)\/(Z[A-Z0-9]{7})\.html";
        /* language=regex */
        const string SHORT_SONG_URL_REGEX_PATTERN = @"zingmp3\.vn\/bai-hat\/(Z[A-Z0-9]{7})\.html";
        /* language=regex */
        const string SONG_VIDEO_ID_REGEX_PATTERN = @"^Z[A-Z0-9]{7}$";
        /* language=regex */
        const string VIDEO_URL_REGEX_PATTERN = @"zingmp3\.vn\/video-clip\/(.*?)\/(Z[A-Z0-9]{7})\.html";

#if NET7_0_OR_GREATER
        [GeneratedRegex(MAIN_MIN_JS_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex MainJSRegex();
        internal static Regex MainMinJS => MainJSRegex();

        [GeneratedRegex(APIKEY_SECRET_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex APIKeySecretRegex();
        internal static Regex ApiKeySecret => APIKeySecretRegex();

        [GeneratedRegex(ALBUM_PLAYLIST_ID_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex AlbumIDRegex();
        internal static Regex AlbumID => AlbumIDRegex();

        [GeneratedRegex(ALBUM_URL_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex AlbumUrlRegex();
        internal static Regex AlbumUrl => AlbumUrlRegex();
        
        [GeneratedRegex(SHORT_ALBUM_URL_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex ShortAlbumUrlRegex();
        internal static Regex ShortAlbumUrl => ShortAlbumUrlRegex();

        [GeneratedRegex(ARTIST_URL_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex ArtistUrlRegex();
        internal static Regex ArtistUrl => ArtistUrlRegex();

        [GeneratedRegex(ARTIST_GENRE_WLB_ID_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex ArtistIDRegex();
        internal static Regex ArtistID => ArtistIDRegex();
        
        [GeneratedRegex(ARTIST_ALIAS_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex ArtistAliasRegex();
        internal static Regex ArtistAlias => ArtistAliasRegex();

        [GeneratedRegex(ARTIST_GENRE_WLB_ID_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex GerneIDRegex();
        internal static Regex GenreID => GerneIDRegex();

        [GeneratedRegex(ALBUM_PLAYLIST_ID_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex PlaylistIDRegex();
        internal static Regex PlaylistID => PlaylistIDRegex();

        [GeneratedRegex(PLAYLIST_URL_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex PlaylistUrlRegex();
        internal static Regex PlaylistUrl => PlaylistUrlRegex();

        [GeneratedRegex(SONG_URL_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex SongUrlRegex();
        internal static Regex SongUrl => SongUrlRegex();

        [GeneratedRegex(SHORT_SONG_URL_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex ShortSongUrlRegex();
        internal static Regex ShortSongUrl => ShortSongUrlRegex();
        
        [GeneratedRegex(SONG_VIDEO_ID_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex SongIDRegex();
        internal static Regex SongID => SongIDRegex();

        [GeneratedRegex(VIDEO_URL_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex VideoUrlRegex();
        internal static  Regex VideoUrl => VideoUrlRegex();

        [GeneratedRegex(SONG_VIDEO_ID_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex VideoIDRegex();
        internal static Regex VideoID => VideoIDRegex();

        [GeneratedRegex(ARTIST_GENRE_WLB_ID_REGEX_PATTERN, RegexOptions.Compiled)]
        private static partial Regex WeeklyLeaderboardIDRegex();
        internal static Regex WeeklyLeaderboardID => WeeklyLeaderboardIDRegex();
#else
        internal static readonly Regex MainMinJS = new Regex(MAIN_MIN_JS_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex ApiKeySecret = new Regex(APIKEY_SECRET_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex AlbumID = new Regex(ALBUM_PLAYLIST_ID_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex AlbumUrl = new Regex(ALBUM_URL_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex ShortAlbumUrl = new Regex(SHORT_ALBUM_URL_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex ArtistUrl = new Regex(ARTIST_URL_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex ArtistID = new Regex(ARTIST_GENRE_WLB_ID_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex ArtistAlias = new Regex(ARTIST_ALIAS_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex GenreID = new Regex(ARTIST_GENRE_WLB_ID_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex PlaylistID = new Regex(ALBUM_PLAYLIST_ID_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex PlaylistUrl = new Regex(PLAYLIST_URL_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex SongUrl = new Regex(SONG_URL_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex ShortSongUrl = new Regex(SHORT_SONG_URL_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex SongID = new Regex(SONG_VIDEO_ID_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex VideoUrl = new Regex(VIDEO_URL_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex VideoID = new Regex(SONG_VIDEO_ID_REGEX_PATTERN, RegexOptions.Compiled);
        internal static readonly Regex WeeklyLeaderboardID = new Regex(ARTIST_GENRE_WLB_ID_REGEX_PATTERN, RegexOptions.Compiled);
#endif
    }
}
