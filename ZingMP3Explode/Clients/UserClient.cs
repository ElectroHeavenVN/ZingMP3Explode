﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Entities;

namespace ZingMP3Explode.Clients
{
    /// <summary>
    /// <para xml:lang="en">Provides methods to access information about the current user.</para>
    /// <para xml:lang="vi">Cung cấp các phương thức để truy cập thông tin về người dùng hiện tại.</para>
    /// </summary>
    public class UserClient
    {
        readonly ZingMP3Client zClient;

        internal UserClient(ZingMP3Client client)
        {
            zClient = client;
        }

        /// <summary>
        /// <para xml:lang="en">Gets information about the current user.</para>
        /// <para xml:lang="vi">Lấy thông tin về người dùng hiện tại.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">The current user information.</para>
        /// <para xml:lang="vi">Thông tin người dùng hiện tại.</para>
        /// </returns>
        public async Task<CurrentUser> GetAsync(CancellationToken cancellationToken = default)
        {
            return await zClient.APIClient.GetCurrentUserAsync(cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the list of favorite songs of the current user.</para>
        /// <para xml:lang="vi">Lấy danh sách bài hát yêu thích của người dùng hiện tại.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">An asynchronous enumerable of favorite songs.</para>
        /// <para xml:lang="vi">Luồng bất đồng bộ chứa các bài hát yêu thích.</para>
        /// </returns>
        public async IAsyncEnumerable<Song> GetFavoriteSongsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var song in zClient.APIClient.GetMyFavoriteSongsAsync(cancellationToken))
                yield return song;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the list of favorite albums of the current user.</para>
        /// <para xml:lang="vi">Lấy danh sách album yêu thích của người dùng hiện tại.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">An asynchronous enumerable of favorite albums.</para>
        /// <para xml:lang="vi">Luồng bất đồng bộ chứa các album yêu thích.</para>
        /// </returns>
        public async IAsyncEnumerable<Album> GetFavoriteAlbumsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var album in zClient.APIClient.GetMyFavoriteAlbumsAsync(cancellationToken))
                yield return album;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the list of favorite MVs of the current user.</para>
        /// <para xml:lang="vi">Lấy danh sách MV yêu thích của người dùng hiện tại.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">An asynchronous enumerable of favorite MVs.</para>
        /// <para xml:lang="vi">Luồng bất đồng bộ chứa các MV yêu thích.</para>
        /// </returns>
        public async IAsyncEnumerable<IncompleteVideo> GetFavoriteMVsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var mv in zClient.APIClient.GetMyFavoriteMVsAsync(cancellationToken))
                yield return mv;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the list of blocked songs of the current user.</para>
        /// <para xml:lang="vi">Lấy danh sách bài hát bị chặn của người dùng hiện tại.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">An asynchronous enumerable of blocked songs.</para>
        /// <para xml:lang="vi">Luồng bất đồng bộ chứa các bài hát bị chặn.</para>
        /// </returns>
        public async IAsyncEnumerable<Song> GetBlockedSongsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var song in zClient.APIClient.GetMyBlockedSongsAsync(cancellationToken))
                yield return song;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the list of blocked artists of the current user.</para>
        /// <para xml:lang="vi">Lấy danh sách nghệ sĩ bị chặn của người dùng hiện tại.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">An asynchronous enumerable of blocked artists.</para>
        /// <para xml:lang="vi">Luồng bất đồng bộ chứa các nghệ sĩ bị chặn.</para>
        /// </returns>
        public async IAsyncEnumerable<IncompleteArtist> GetBlockedArtistsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var artist in zClient.APIClient.GetMyBlockedArtistsAsync(cancellationToken))
                yield return artist;
        }
    }
}
