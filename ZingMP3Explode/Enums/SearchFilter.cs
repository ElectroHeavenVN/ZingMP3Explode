namespace ZingMP3Explode.Enums
{
    /// <summary>
    /// Filter applied to a Soundcloud search query.
    /// </summary>
    public enum SearchFilter
    {
        /// <summary>
        /// No filter applied.
        /// </summary>
        None,

        /// <summary>
        /// Only search for songs.
        /// </summary>
        Song,

        /// <summary>
        /// Search for playlists and albums.
        /// </summary>
        PlaylistAndAlbums,

        ///// <summary>
        ///// Only search for playlists.
        ///// </summary>
        //Playlist,

        ///// <summary>
        ///// Only search for albums.
        ///// </summary>
        //Album,

        /// <summary>
        /// Only search for artists/OAs.
        /// </summary>
        Artist,

        /// <summary>
        /// Only search for video/MV.
        /// </summary>
        Video
    }
}
