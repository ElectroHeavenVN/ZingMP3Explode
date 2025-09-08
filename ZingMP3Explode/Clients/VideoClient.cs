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

        /// <summary>
        /// <para xml:lang="en">Checks if the given URL is a valid video URL.</para>
        /// <para xml:lang="vi">Kiểm tra xem URL đã cho có phải là URL video hợp lệ hay không.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The URL to check.</para>
        /// <para xml:lang="vi">URL cần kiểm tra.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">True if the URL is a valid video URL.</para>
        /// <para xml:lang="vi">Đúng nếu URL là URL video hợp lệ.</para>
        /// </returns>
        public bool IsUrlValid(string url) => IsUrlValid(url, out _);

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
        /// <para xml:lang="en">Gets video information from a given URL.</para>
        /// <para xml:lang="vi">Lấy thông tin video từ một URL đã cho.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The video URL.</para>
        /// <para xml:lang="vi">URL video.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The video information.</para>
        /// <para xml:lang="vi">Thông tin video.</para>
        /// </returns>
        public async Task<Video> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid video url");
            return await GetByIDAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets video information by its ID.</para>
        /// <para xml:lang="vi">Lấy thông tin video từ ID.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The video ID.</para>
        /// <para xml:lang="vi">ID video.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The video information.</para>
        /// <para xml:lang="vi">Thông tin video.</para>
        /// </returns>
        public async Task<Video> GetByIDAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!Regexes.VideoID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid video id");
            return await zClient.APIClient.GetVideoAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets related video sections from a given URL.</para>
        /// <para xml:lang="vi">Lấy các phần video liên quan từ một URL đã cho.</para>
        /// </summary>
        /// <param name="url">
        /// <para xml:lang="en">The video URL.</para>
        /// <para xml:lang="vi">URL video.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A list of related video sections.</para>
        /// <para xml:lang="vi">Danh sách các phần video liên quan.</para>
        /// </returns>
        public async Task<List<PageSection<Video>>> GetRelatedSectionsAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid video url");
            return await GetRelatedSectionsByIDAsync(id, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets related video sections by video ID.</para>
        /// <para xml:lang="vi">Lấy các phần video liên quan từ ID video.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The video ID.</para>
        /// <para xml:lang="vi">ID video.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A list of related video sections.</para>
        /// <para xml:lang="vi">Danh sách các phần video liên quan.</para>
        /// </returns>
        public async Task<List<PageSection<Video>>> GetRelatedSectionsByIDAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!Regexes.VideoID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid video id");
            return await zClient.APIClient.GetVideoRelatedSectionsAsync(id, cancellationToken);
        }
    }
}
