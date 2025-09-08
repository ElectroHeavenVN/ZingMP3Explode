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
        /// <para xml:lang="en">Gets the playlist information from the given URL or ID.</para>
        /// <para xml:lang="vi">Lấy thông tin danh sách phát từ URL hoặc ID.</para>
        /// </summary>
        /// <param name="idOrUrl">
        /// <para xml:lang="en">The playlist URL or ID.</para>
        /// <para xml:lang="vi">URL hoặc ID danh sách phát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The playlist information.</para>
        /// <para xml:lang="vi">Thông tin danh sách phát.</para>
        /// </returns>
        public async Task<Playlist> GetAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(idOrUrl, out string id))
                id = idOrUrl;
            if (!Regexes.PlaylistID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid playlist ID/URL");
            return await zClient.APIClient.GetPlaylistAsync(id, cancellationToken);
        }
    }
}
