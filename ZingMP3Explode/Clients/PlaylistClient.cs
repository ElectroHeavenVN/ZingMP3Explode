using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Entities;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Clients
{
    /// <summary>
    /// <para xml:lang="en">Provides methods to access playlist information.</para>
    /// <para xml:lang="vi">Cung cấp các phương thức để truy cập thông tin danh sách phát.</para>
    /// </summary>
    public class PlaylistClient
    {
        readonly ZingMP3Client zClient;

        internal PlaylistClient(ZingMP3Client client)
        {
            zClient = client;
        }

        /// <summary>
        /// <para xml:lang="en">Checks if the given URL is a valid playlist URL.</para>
        /// <para xml:lang="vi">Kiểm tra xem URL đã cho có phải là URL danh sách phát hợp lệ hay không.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The URL to check.</para>
        /// <para xml:lang="vi">URL cần kiểm tra.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">True if the URL is a valid playlist URL.</para>
        /// <para xml:lang="vi">Đúng nếu URL là URL danh sách phát hợp lệ.</para>
        /// </returns>
        public bool IsUrlValid(string url) => IsUrlValid(url, out _);

        static bool IsUrlValid(string url, out string id)
        {
            id = "";
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;
            Match match = Regexes.PlaylistUrl.Match(url);
            if (match.Success)
            {
                id = match.Groups[2].Value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the playlist information from the given URL.</para>
        /// <para xml:lang="vi">Lấy thông tin danh sách phát từ URL đã cho.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The playlist URL.</para>
        /// <para xml:lang="vi">URL danh sách phát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The playlist information.</para>
        /// <para xml:lang="vi">Thông tin danh sách phát.</para>
        /// </returns>
        public async Task<Playlist> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid playlist url");
            return await GetByIDAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the playlist information by its ID.</para>
        /// <para xml:lang="vi">Lấy thông tin danh sách phát theo ID.</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ZingMP3ExplodeException"></exception>
        public async Task<Playlist> GetByIDAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!Regexes.PlaylistID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid playlist id");
            return await zClient.APIClient.GetPlaylistAsync(id, cancellationToken);
        }
    }
}
