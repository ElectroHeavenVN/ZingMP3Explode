using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Entities.Genres
{
    /// <summary>
    /// <para xml:lang="en">Provides methods to access genre information.</para>
    /// <para xml:lang="vi">Cung cấp các phương thức để truy cập thông tin thể loại nhạc.</para>
    /// </summary>
    public class GenreClient
    {
        readonly ZingMP3Client zClient;

        internal GenreClient(ZingMP3Client client)
        {
            zClient = client;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the genre information by its ID.</para>
        /// <para xml:lang="vi">Lấy thông tin thể loại nhạc theo ID.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The genre ID.</para>
        /// <para xml:lang="vi">ID thể loại nhạc.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The genre information.</para>
        /// <para xml:lang="vi">Thông tin thể loại nhạc.</para>
        /// </returns>
        public async Task<Genre> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!Regexes.GenreID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid genre ID");
            return await zClient.APIClient.GetGerneAsync(id, cancellationToken);
        }
    }
}
