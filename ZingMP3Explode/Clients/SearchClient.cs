using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Entities;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Clients
{
    /// <summary>
    /// <para xml:lang="en">Provides methods to search for songs, artists, albums, playlists, and videos.</para>
    /// <para xml:lang="vi">Cung cấp các phương thức để tìm kiếm bài hát, nghệ sĩ, album, playlist và video.</para>
    /// </summary>
    public class SearchClient
    {
        readonly ZingMP3Client zClient;

        internal SearchClient(ZingMP3Client client)
        {
            zClient = client;
        }

        /// <summary>
        /// <para xml:lang="en">Searches for all types (songs, artists, albums, playlists, videos) based on the query.</para>
        /// <para xml:lang="vi">Tìm kiếm tất cả các loại (bài hát, nghệ sĩ, album, playlist, video) dựa trên truy vấn.</para>
        /// </summary>
        /// <param name="query">
        /// <para xml:lang="en">The search query.</para>
        /// <para xml:lang="vi">Truy vấn tìm kiếm.</para>
        /// </param>
        /// <param name="allowCorrection">
        /// <para xml:lang="en">Whether to allow search query correction.</para>
        /// <para xml:lang="vi">Có cho phép sửa lỗi trong truy vấn tìm kiếm hay không.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The search results containing various types.</para>
        /// <para xml:lang="vi">Kết quả tìm kiếm chứa các loại khác nhau.</para>
        /// </returns>
        public async Task<MultiSearchResult> SearchMultiAsync(string query, bool allowCorrection = true, CancellationToken cancellationToken = default)
        {
            return await zClient.APIClient.SearchMultiAsync(query, allowCorrection, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Searches for items based on the query and filter.</para>
        /// <para xml:lang="vi">Tìm kiếm các mục dựa trên truy vấn và bộ lọc.</para>
        /// </summary>
        /// <param name="query">
        /// <para xml:lang="en">The search query.</para>
        /// <para xml:lang="vi">Truy vấn tìm kiếm.</para>
        /// </param>
        /// <param name="filter">
        /// <para xml:lang="en">The search filter to specify the type of items to search for.</para>
        /// <para xml:lang="vi">Bộ lọc tìm kiếm để chỉ định loại mục cần tìm kiếm.</para>
        /// </param>
        /// <param name="allowCorrection">
        /// <para xml:lang="en">Whether to allow search query correction.</para>
        /// <para xml:lang="vi">Có cho phép sửa lỗi trong truy vấn tìm kiếm hay không.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">An asynchronous enumerable of search results matching the specified filter.</para>
        /// <para xml:lang="vi">Một luồng bất đồng bộ của các kết quả tìm kiếm phù hợp với bộ lọc đã chỉ định.</para>
        /// </returns>
        public async IAsyncEnumerable<ISearchable> SearchAsync(string query, SearchFilter filter, bool allowCorrection = true, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var item in zClient.APIClient.SearchAsync(query, filter, allowCorrection, cancellationToken))
                yield return item;
        }

        /// <summary>
        /// <para xml:lang="en">Searches for items of a specific type based on the query.</para>
        /// <para xml:lang="vi">Tìm kiếm các mục của một loại cụ thể dựa trên truy vấn.</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para xml:lang="en">The type of items to search for. Must implement the interface <see cref="ISearchable"/>.</para>
        /// <para xml:lang="vi">Loại mục cần tìm kiếm. Phải triển khai interface <see cref="ISearchable"/>.</para>
        /// </typeparam>
        /// <param name="query">
        /// <para xml:lang="en">The search query.</para>
        /// <para xml:lang="vi">Truy vấn tìm kiếm.</para>
        /// </param>
        /// <param name="allowCorrection">
        /// <para xml:lang="en">Whether to allow search query correction.</para>
        /// <para xml:lang="vi">Có cho phép sửa lỗi trong truy vấn tìm kiếm hay không.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">An asynchronous enumerable of search results of the specified type.</para>
        /// <para xml:lang="vi">Một luồng bất đồng bộ của các kết quả tìm kiếm thuộc loại đã chỉ định.</para>
        /// </returns>
        public async IAsyncEnumerable<T> SearchAsync<T>(string query, bool allowCorrection = true, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : ISearchable
        {
            SearchFilter filter;
            if (typeof(T) == typeof(Artist))
                filter = SearchFilter.Artist;
            else if (typeof(T) == typeof(Song))
                filter = SearchFilter.Song;
            else if (typeof(T) == typeof(Album) || typeof(T) == typeof(Playlist))
                filter = SearchFilter.PlaylistAndAlbums;
            else if (typeof(T) == typeof(Video))
                filter = SearchFilter.Video;
            else
                throw new ZingMP3ExplodeException("Invalid search type");
            await foreach (var item in SearchAsync(query, filter, allowCorrection, cancellationToken))
            {
                if (item is T t)
                    yield return t;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Searches for items based on the query and filter, returning a list of results up to the specified count.</para>
        /// <para xml:lang="vi">Tìm kiếm các mục dựa trên truy vấn và bộ lọc, trả về danh sách kết quả lên đến số lượng đã chỉ định.</para>
        /// </summary>
        /// <param name="query">
        /// <para xml:lang="en">The search query.</para>
        /// <para xml:lang="vi">Truy vấn tìm kiếm.</para>
        /// </param>
        /// <param name="filter">
        /// <para xml:lang="en">The search filter to specify the type of items to search for.</para>
        /// <para xml:lang="vi">Bộ lọc tìm kiếm để chỉ định loại mục cần tìm kiếm.</para>
        /// </param>
        /// <param name="count">
        /// <para xml:lang="en">The maximum number of results to return.</para>
        /// <para xml:lang="vi">Số lượng kết quả tối đa để trả về.</para>
        /// </param>
        /// <param name="allowCorrection">
        /// <para xml:lang="en">Whether to allow search query correction.</para>
        /// <para xml:lang="vi">Có cho phép sửa lỗi trong truy vấn tìm kiếm hay không.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A list of search results matching the specified filter, limited to the specified count.</para>
        /// <para xml:lang="vi">Một danh sách các kết quả tìm kiếm phù hợp với bộ lọc đã chỉ định, giới hạn theo số lượng đã chỉ định.</para>
        /// </returns>
        public async Task<List<ISearchable>> SearchAsync(string query, SearchFilter filter, int count = 18, bool allowCorrection = true, CancellationToken cancellationToken = default)
        {
            List<ISearchable> results = [];
            await foreach (var item in SearchAsync(query, filter, allowCorrection, cancellationToken))
            {
                results.Add(item);
                if (results.Count >= count)
                    break;
            }
            return results;
        }

        /// <summary>
        /// <para xml:lang="en">Searches for items of a specific type based on the query, returning a list of results up to the specified count.</para>
        /// <para xml:lang="vi">Tìm kiếm các mục của một loại cụ thể dựa trên truy vấn, trả về danh sách kết quả lên đến số lượng đã chỉ định.</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para xml:lang="en">The type of items to search for. Must implement the interface <see cref="ISearchable"/>.</para>
        /// <para xml:lang="vi">Loại mục cần tìm kiếm. Phải triển khai interface <see cref="ISearchable"/>.</para>
        /// </typeparam>
        /// <param name="query">
        /// <para xml:lang="en">The search query.</para>
        /// <para xml:lang="vi">Truy vấn tìm kiếm.</para>
        /// </param>
        /// <param name="count">
        /// <para xml:lang="en">The maximum number of results to return.</para>
        /// <para xml:lang="vi">Số lượng kết quả tối đa để trả về.</para>
        /// </param>
        /// <param name="allowCorrection">
        /// <para xml:lang="en">Whether to allow search query correction.</para>
        /// <para xml:lang="vi">Có cho phép sửa lỗi trong truy vấn tìm kiếm hay không.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A list of search results of the specified type, limited to the specified count.</para>
        /// <para xml:lang="vi">Một danh sách các kết quả tìm kiếm thuộc loại đã chỉ định, giới hạn theo số lượng đã chỉ định.</para>
        /// </returns>
        public async Task<List<T>> SearchAsync<T>(string query, int count = 18, bool allowCorrection = true, CancellationToken cancellationToken = default) where T : ISearchable
        {
            List<T> results = [];
            await foreach (var item in SearchAsync<T>(query, allowCorrection, cancellationToken))
            {
                results.Add(item);
                if (results.Count >= count)
                    break;
            }
            return results;
        }
    }
}
