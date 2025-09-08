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
    /// <para xml:lang="en">Provides methods to access album information.</para>
    /// <para xml:lang="vi">Cung cấp các phương thức để truy cập thông tin album.</para>
    /// </summary>
    public class AlbumClient
    {
        readonly ZingMP3Client zClient;

        internal AlbumClient(ZingMP3Client client)
        {
            zClient = client;
        }

        /// <summary>
        /// <para xml:lang="en">Checks if the given URL is a valid album URL.</para>
        /// <para xml:lang="vi">Kiểm tra xem URL đã cho có phải là URL album hợp lệ hay không.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The URL to check.</para>
        /// <para xml:lang="vi">URL cần kiểm tra.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">True if the URL is a valid album URL.</para>
        /// <para xml:lang="vi">Đúng nếu URL là URL album hợp lệ.</para>
        /// </returns>
        public bool IsUrlValid(string url) => IsUrlValid(url, out _);

        static bool IsUrlValid(string url, out string id)
        {
            id = "";
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;
            Match match = Regexes.AlbumUrl.Match(url);
            if (match.Success)
            {
                id = match.Groups[2].Value;
                return true;
            }
            match = Regexes.ShortAlbumUrl.Match(url);
            if (match.Success)
            {
                id = match.Groups[1].Value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// <para xml:lang="en">Gets album information from a given URL.</para>
        /// <para xml:lang="vi">Lấy thông tin album từ một URL đã cho.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The album URL.</para>
        /// <para xml:lang="vi">URL album.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The album information.</para>
        /// <para xml:lang="vi">Thông tin album.</para>
        /// </returns>
        public async Task<Album> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid album url");
            return await GetByIDAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets album information by its ID.</para>
        /// <para xml:lang="vi">Lấy thông tin album từ ID.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The album ID.</para>
        /// <para xml:lang="vi">ID album.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The album information.</para>
        /// <para xml:lang="vi">Thông tin album.</para>
        /// </returns>
        public async Task<Album> GetByIDAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!Regexes.AlbumID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid album id");
            return await zClient.APIClient.GetAlbumAsync(id, cancellationToken);
        }
    }
}
