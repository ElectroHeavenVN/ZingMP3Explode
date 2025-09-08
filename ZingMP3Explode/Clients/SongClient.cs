using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Entities.Songs
{
    /// <summary>
    /// <para xml:lang="en">Provides methods to access song information.</para>
    /// <para xml:lang="vi">Cung cấp các phương thức để truy cập thông tin bài hát.</para>
    /// </summary>
    public class SongClient
    {
        readonly ZingMP3Client zClient;

        internal SongClient(ZingMP3Client client)
        {
            zClient = client;
        }

        static bool IsUrlValid(string url, out string id)
        {
            id = "";
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;
            Match match = Regexes.SongUrl.Match(url);
            if (match.Success)
            {
                id = match.Groups[2].Value;
                return true;
            }
            match = Regexes.ShortSongUrl.Match(url);
            if (match.Success)
            {
                id = match.Groups[1].Value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the song information from the given URL or ID.</para>
        /// <para xml:lang="vi">Lấy thông tin bài hát từ URL hoặc ID.</para>
        /// </summary>
        /// <param name="idOrUrl">
        /// <para xml:lang="en">The song URL or ID.</para>
        /// <para xml:lang="vi">URL hoặc ID bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The song information.</para>
        /// <para xml:lang="vi">Thông tin bài hát.</para>
        /// </returns>
        public async Task<Song> GetAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(idOrUrl, out string id))
                id = idOrUrl;
            if (!Regexes.SongID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid song ID/URL");
            return await zClient.APIClient.GetSongAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the audio stream url of the song from the given URL or ID.</para>
        /// <para xml:lang="vi">Lấy liên kết tệp âm thanh của bài hát từ URL hoặc ID.</para>
        /// </summary>
        /// <param name="idOrUrl">
        /// <para xml:lang="en">The song URL or ID.</para>
        /// <para xml:lang="vi">URL hoặc ID bài hát.</para>
        /// </param>
        /// <param name="quality">
        /// <para xml:lang="en">The desired audio quality.</para>
        /// <para xml:lang="vi">Chất lượng âm thanh mong muốn.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The audio stream url.</para>
        /// <para xml:lang="vi">Liên kết tệp âm thanh.</para>
        /// </returns>
        public async Task<string> GetAudioStreamUrlAsync(string idOrUrl, AudioQuality quality = AudioQuality.Best, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(idOrUrl, out string id))
                id = idOrUrl;
            if (!Regexes.SongID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid song ID/URL");
            return await zClient.APIClient.GetAudioStreamUrlAsync(id, quality, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets all available audio stream urls of the song from the given URL or ID.</para>
        /// <para xml:lang="vi">Lấy tất cả các liên kết tệp âm thanh có sẵn của bài hát từ URL hoặc ID.</para>
        /// </summary>
        /// <param name="idOrUrl">
        /// <para xml:lang="en">The song URL or ID.</para>
        /// <para xml:lang="vi">URL hoặc ID bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A dictionary containing audio qualities and their corresponding stream urls.</para>
        /// <para xml:lang="vi">Một từ điển chứa các chất lượng âm thanh và liên kết tệp âm thanh tương ứng.</para>
        /// </returns>
        public async Task<Dictionary<AudioQuality, string>> GetAllAudioStreamUrlsAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(idOrUrl, out string id))
                id = idOrUrl;
            if (!Regexes.SongID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid song ID/URL");
            return await zClient.APIClient.GetAllAudioStreamUrlsAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the lyrics of the song by its ID or URL.</para>
        /// <para xml:lang="vi">Lấy lời bài hát theo ID hoặc URL.</para>
        /// </summary>
        /// <param name="idOrUrl">
        /// <para xml:lang="en">The song URL or ID.</para>
        /// <para xml:lang="vi">URL hoặc ID bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The lyrics data of the song.</para>
        /// <para xml:lang="vi">Dữ liệu lời bài hát.</para>
        /// </returns>
        public async Task<LyricData> GetLyricsAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(idOrUrl, out string id))
                id = idOrUrl;
            if (!Regexes.SongID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid song ID/URL");
            return await zClient.APIClient.GetLyricsAsync(id, cancellationToken);
        }
    }
}
