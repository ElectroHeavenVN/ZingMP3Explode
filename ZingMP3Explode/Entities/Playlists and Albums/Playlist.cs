using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about a playlist.</para>
    /// <para xml:lang="vi">Thông tin về một danh sách phát.</para>
    /// </summary>
    public class Playlist : BaseList
    {
        [JsonConstructor]
        internal Playlist() { }

        internal Playlist(BaseList baseList)
        {
            ID = baseList.ID;
            Title = baseList.Title;
            BigThumbnailUrl = baseList.BigThumbnailUrl;
            ThumbnailUrl = baseList.ThumbnailUrl;
            IsOfficial = baseList.IsOfficial;
            Url = baseList.Url;
            IsIndie = baseList.IsIndie;
            ReleaseDate = baseList.ReleaseDate;
            ShortDescription = baseList.ShortDescription;
            Description = baseList.Description;
            releaseDateUnix = baseList.releaseDateUnix;
            genreIDs = baseList.genreIDs;
            artists = [.. baseList.artists];
            AllArtistsNames = baseList.AllArtistsNames;
            Uid = baseList.Uid;
            IsPrivate = baseList.IsPrivate;
            Username = baseList.Username;
            IsAlbum = baseList.IsAlbum;
            Type = baseList.Type;
            IsSingle = baseList.IsSingle;
            Distributor = baseList.Distributor;
            Alias = baseList.Alias;
            ContentLastUpdate = baseList.ContentLastUpdate;
            genres = [.. baseList.genres];
            Songs = baseList.Songs;
            Likes = baseList.Likes;
            Listens = baseList.Listens;
            Liked = baseList.Liked;
            PlayItemMode = baseList.PlayItemMode;
            SubType = baseList.SubType;
            IsShuffle = baseList.IsShuffle;
            SectionID = baseList.SectionID;
            IsPR = baseList.IsPR;

        }
    }
}
