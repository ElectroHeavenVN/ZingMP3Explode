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
        /// <para xml:lang="en">Gets album information from a given URL or ID.</para>
        /// <para xml:lang="vi">Lấy thông tin album từ một URL hoặc ID.</para>
        /// </summary>
        /// <param name="idOrUrl">
        /// <para xml:lang="en">The album URL or ID.</para>
        /// <para xml:lang="vi">URL hoặc ID album.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The album information.</para>
        /// <para xml:lang="vi">Thông tin album.</para>
        /// </returns>
        public async Task<Album> GetAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(idOrUrl, out string id))
                id = idOrUrl;
            if (!Regexes.AlbumID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid album ID/URL");
            return await zClient.APIClient.GetAlbumAsync(id, cancellationToken);
        }
    }
}
