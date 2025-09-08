namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Filter applied to a search query.</para>
    /// <para xml:lang="vi">Bộ lọc được áp dụng cho truy vấn tìm kiếm.</para>
    /// </summary>
    public enum SearchFilter
    {
        /// <summary>
        /// <para xml:lang="en">No filter applied.</para>
        /// <para xml:lang="vi">Không áp dụng bộ lọc nào.</para>
        /// </summary>
        None,

        /// <summary>
        /// <para xml:lang="en">Only search for songs.</para>
        /// <para xml:lang="vi">Chỉ tìm kiếm bài hát.</para>
        /// </summary>
        Song,

        /// <summary>
        /// <para xml:lang="en">Only search for playlists and albums.</para>
        /// <para xml:lang="vi">Chỉ tìm kiếm playlist và album.</para>
        /// </summary>
        PlaylistAndAlbums,

        /// <summary>
        /// <para xml:lang="en">Only search for for artists/OAs.</para>
        /// <para xml:lang="vi">Chỉ tìm kiếm nghệ sĩ/OA.</para>
        /// </summary>
        Artist,

        /// <summary>
        /// <para xml:lang="en">Only search for video/MV.</para>
        /// <para xml:lang="vi">Chỉ tìm kiếm video/MV.</para>
        /// </summary>
        Video
    }
}
