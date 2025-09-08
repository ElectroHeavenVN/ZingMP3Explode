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

        /// <summary>
        /// <para xml:lang="en">Checks if the given URL is a valid song URL.</para>
        /// <para xml:lang="vi">Kiểm tra xem URL đã cho có phải là URL bài hát hợp lệ hay không.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The URL to check.</para>
        /// <para xml:lang="vi">URL cần kiểm tra.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">True if the URL is a valid song URL.</para>
        /// <para xml:lang="vi">Đúng nếu URL là URL bài hát hợp lệ.</para>
        /// </returns>
        public bool IsUrlValid(string url) => IsUrlValid(url, out _);

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
            match = Regexes.ShortSonngUrl.Match(url);
            if (match.Success)
            {
                id = match.Groups[1].Value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the song information from the given URL.</para>
        /// <para xml:lang="vi">Lấy thông tin bài hát từ URL đã cho.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The song URL.</para>
        /// <para xml:lang="vi">URL bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The song information.</para>
        /// <para xml:lang="vi">Thông tin bài hát.</para>
        /// </returns>
        public async Task<Song> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid song url");
            return await GetByIDAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the song information by its ID.</para>
        /// <para xml:lang="vi">Lấy thông tin bài hát theo ID.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The song ID.</para>
        /// <para xml:lang="vi">ID bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The song information.</para>
        /// <para xml:lang="vi">Thông tin bài hát.</para>
        /// </returns>
        public async Task<Song> GetByIDAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!Regexes.SongID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid song id");
            return await zClient.APIClient.GetSongAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the audio stream link of the song from the given URL.</para>
        /// <para xml:lang="vi">Lấy liên kết tệp âm thanh của bài hát từ URL đã cho.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The song URL.</para>
        /// <para xml:lang="vi">URL bài hát.</para>
        /// </param>
        /// <param name="quality">
        /// <para xml:lang="en">The desired audio quality.</para>
        /// <para xml:lang="vi">Chất lượng âm thanh mong muốn.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The audio stream link.</para>
        /// <para xml:lang="vi">Liên kết tệp âm thanh.</para>
        /// </returns>
        public async Task<string> GetAudioStreamUrlByUrlAsync(string url, AudioQuality quality = AudioQuality.Best, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid song url");
            return await GetAudioStreamUrlAsync(id, quality, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the audio stream link of the song by its ID.</para>
        /// <para xml:lang="vi">Lấy liên kết tệp âm thanh của bài hát theo ID.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The song ID.</para>
        /// <para xml:lang="vi">ID bài hát.</para>
        /// </param>
        /// <param name="quality">
        /// <para xml:lang="en">The desired audio quality.</para>
        /// <para xml:lang="vi">Chất lượng âm thanh mong muốn.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The audio stream link.</para>
        /// <para xml:lang="vi">Liên kết tệp âm thanh.</para>
        /// </returns>
        public async Task<string> GetAudioStreamUrlAsync(string id, AudioQuality quality = AudioQuality.Best, CancellationToken cancellationToken = default)
        {
            return await zClient.APIClient.GetAudioStreamUrlAsync(id, quality, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets all available audio stream links of the song from the given URL.</para>
        /// <para xml:lang="vi">Lấy tất cả các liên kết tệp âm thanh có sẵn của bài hát từ URL đã cho.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The song URL.</para>
        /// <para xml:lang="vi">URL bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A dictionary containing audio qualities and their corresponding stream links.</para>
        /// <para xml:lang="vi">Một từ điển chứa các chất lượng âm thanh và liên kết tệp âm thanh tương ứng.</para>
        /// </returns>
        public async Task<Dictionary<AudioQuality, string>> GetAllAudioStreamUrlsByUrlAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid song url");
            return await GetAllAudioStreamUrlsAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets all available audio stream links of the song by its ID.</para>
        /// <para xml:lang="vi">Lấy tất cả các liên kết tệp âm thanh có sẵn của bài hát theo ID.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The song ID.</para>
        /// <para xml:lang="vi">ID bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A dictionary containing audio qualities and their corresponding stream links.</para>
        /// <para xml:lang="vi">Một từ điển chứa các chất lượng âm thanh và liên kết tệp âm thanh tương ứng.</para>
        /// </returns>
        public async Task<Dictionary<AudioQuality, string>> GetAllAudioStreamUrlsAsync(string id, CancellationToken cancellationToken = default)
        {
            return await zClient.APIClient.GetAllAudioStreamUrlsAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the lyrics of the song by its ID.</para>
        /// <para xml:lang="vi">Lấy lời bài hát theo ID.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The song ID.</para>
        /// <para xml:lang="vi">ID bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The lyrics data of the song.</para>
        /// <para xml:lang="vi">Dữ liệu lời bài hát.</para>
        /// </returns>
        public async Task<LyricData> GetLyricsAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!Regexes.SongID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid song id");
            return await zClient.APIClient.GetLyricsAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the lyrics of the song from the given URL.</para>
        /// <para xml:lang="vi">Lấy lời bài hát từ URL đã cho.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The song URL.</para>
        /// <para xml:lang="vi">URL bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The lyrics data of the song.</para>
        /// <para xml:lang="vi">Dữ liệu lời bài hát.</para>
        /// </returns>
        public async Task<LyricData> GetLyricsByUrlAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid song url");
            return await GetLyricsAsync(id, cancellationToken);
        }
    }
}
