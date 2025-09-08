using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Entities;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Clients
{
    /// <summary>
    /// <para xml:lang="en">Provides methods to access artist information.</para>
    /// <para xml:lang="vi">Cung cấp các phương thức để truy cập thông tin nghệ sĩ.</para>
    /// </summary>
    public class ArtistClient
    {
        readonly ZingMP3Client zClient;

        internal ArtistClient(ZingMP3Client client)
        {
            zClient = client;
        }

        static bool IsUrlValid(string url, out string alias)
        {
            alias = "";
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;
            Match match = Regexes.ArtistUrl.Match(url);
            if (match.Success)
            {
                alias = match.Groups[1].Value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the artist information from the given URL or alias.</para>
        /// <para xml:lang="vi">Lấy thông tin nghệ sĩ từ URL hoặc bí danh.</para>
        /// </summary>
        /// <param name="urlOrAlias">
        /// <para xml:lang="en">The artist URL or alias.</para>
        /// <para xml:lang="vi">URL hoặc bí danh nghệ sĩ.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The artist information.</para>
        /// <para xml:lang="vi">Thông tin nghệ sĩ.</para>
        /// </returns>
        public async Task<Artist> GetAsync(string urlOrAlias, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(urlOrAlias, out string alias))
                alias = urlOrAlias;
            if (!Regexes.ArtistAlias.IsMatch(alias))
                throw new ZingMP3ExplodeException("Invalid artist URL/alias");
            return await zClient.APIClient.GetArtistByAliasAsync(alias, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the songs of the artist by the given artist ID.</para>
        /// <para xml:lang="vi">Lấy các bài hát của nghệ sĩ theo ID nghệ sĩ đã cho.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The artist ID.</para>
        /// <para xml:lang="vi">ID nghệ sĩ.</para>
        /// </param>
        /// <param name="sortType">
        /// <para xml:lang="en">The sort type of the songs.</para>
        /// <para xml:lang="vi">Loại sắp xếp của các bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">An async enumerable of songs.</para>
        /// <para xml:lang="vi">Một luồng bất đồng bộ chứa các bài hát.</para>
        /// </returns>
        public async IAsyncEnumerable<Song> GetSongsAsync(string id, SortType sortType = SortType.Popular, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (!Regexes.ArtistID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid artist ID");
            await foreach (var song in zClient.APIClient.GetSongsAsync(id, sortType, cancellationToken))
                yield return song;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the MVs of the artist by the given artist ID.</para>
        /// <para xml:lang="vi">Lấy các MV âm nhạc của nghệ sĩ theo ID nghệ sĩ đã cho.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The artist ID.</para>
        /// <para xml:lang="vi">ID nghệ sĩ.</para>
        /// </param>
        /// <param name="sortType">
        /// <para xml:lang="en">The sort type of the MVs.</para>
        /// <para xml:lang="vi">Loại sắp xếp của các MV.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">An async enumerable of MVs.</para>
        /// <para xml:lang="vi">Một luồng bất đồng bộ chứa các MV.</para>
        /// </returns>
        public async IAsyncEnumerable<IncompleteVideo> GetMVsAsync(string id, SortType sortType = SortType.Popular, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (!Regexes.ArtistID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid artist ID");
            await foreach (var video in zClient.APIClient.GetMVsAsync(id, sortType, cancellationToken))
                yield return video;
        }

        /// <summary>
        /// <para xml:lang="en">Gets all songs of the artist by the given artist ID.</para>
        /// <para xml:lang="vi">Lấy tất cả các bài hát của nghệ sĩ theo ID nghệ sĩ đã cho.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The artist ID.</para>
        /// <para xml:lang="vi">ID nghệ sĩ.</para>
        /// </param>
        /// <param name="sortType">
        /// <para xml:lang="en">The sort type of the songs.</para>
        /// <para xml:lang="vi">Loại sắp xếp của các bài hát.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A list of songs.</para>
        /// <para xml:lang="vi">Một danh sách các bài hát.</para>
        /// </returns>
        public async Task<List<Song>> GetAllSongsAsync(string id, SortType sortType = SortType.Popular, CancellationToken cancellationToken = default)
        {
            List<Song> songs = [];
            await foreach (var song in GetSongsAsync(id, sortType, cancellationToken))
                songs.Add(song);
            return songs;
        }

        /// <summary>
        /// <para xml:lang="en">Gets all MVs of the artist by the given artist ID.</para>
        /// <para xml:lang="vi">Lấy tất cả các MV của nghệ sĩ theo ID nghệ sĩ đã cho.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The artist ID.</para>
        /// <para xml:lang="vi">ID nghệ sĩ.</para>
        /// </param>
        /// <param name="sortType">
        /// <para xml:lang="en">The sort type of the MVs.</para>
        /// <para xml:lang="vi">Loại sắp xếp của các MV.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">A list of MVs.</para>
        /// <para xml:lang="vi">Một danh sách các MV.</para>
        /// </returns>
        public async Task<List<IncompleteVideo>> GetAllMVsAsync(string id, SortType sortType = SortType.Popular, CancellationToken cancellationToken = default)
        {
            List<IncompleteVideo> videos = [];
            await foreach (var video in GetMVsAsync(id, sortType, cancellationToken))
                videos.Add(video);
            return videos;
        }
    }
}
