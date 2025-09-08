using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Entities.Videos
{
    /// <summary>
    /// <para xml:lang="en">Provides methods to access video information.</para>
    /// <para xml:lang="vi">Cung cấp các phương thức để truy cập thông tin video.</para>
    /// </summary>
    public class VideoClient
    {
        readonly ZingMP3Client zClient;

        internal VideoClient(ZingMP3Client client)
        {
            zClient = client;
        }

        static bool IsUrlValid(string url, out string id)
        {
            id = "";
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;
            Match match = Regexes.VideoUrl.Match(url);
            if (match.Success)
            {
                id = match.Groups[2].Value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// <para xml:lang="en">Gets video information from a given URL or ID.</para>
        /// <para xml:lang="vi">Lấy thông tin video từ một URL hoặc ID.</para>
        /// </summary>
        /// <param name="idOrUrl">
        /// <para xml:lang="en">The video URL or ID.</para>
        /// <para xml:lang="vi">URL hoặc ID video.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The video information.</para>
        /// <para xml:lang="vi">Thông tin video.</para>
        /// </returns>
        public async Task<Video> GetAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(idOrUrl, out string id))
                id = idOrUrl;
            if (!Regexes.VideoID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid video ID/URL");
            return await zClient.APIClient.GetVideoAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets related video sections from a given URL or ID.</para>
        /// <para xml:lang="vi">Lấy các phần video liên quan từ một URL hoặc ID.</para>
        /// </summary>
        /// <param name="idOrUrl">
        /// <para xml:lang="en">The video URL or ID.</para>
        /// <para xml:lang="vi">URL hoặc ID video.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A list of related video sections.</para>
        /// <para xml:lang="vi">Danh sách các phần video liên quan.</para>
        /// </returns>
        public async Task<List<PageSection<Video>>> GetRelatedSectionsAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(idOrUrl, out string id))
                id = idOrUrl;
            if (!Regexes.VideoID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid video ID/URL");
            return await zClient.APIClient.GetVideoRelatedSectionsAsync(id, cancellationToken);
        }
    }
}
