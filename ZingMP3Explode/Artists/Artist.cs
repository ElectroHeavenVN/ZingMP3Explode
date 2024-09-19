using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using ZingMP3Explode.Albums;
using ZingMP3Explode.Converters;
using ZingMP3Explode.Playlists;
using ZingMP3Explode.Sections;
using ZingMP3Explode.Songs;
using ZingMP3Explode.Videos;

namespace ZingMP3Explode.Artists
{
    /// <summary>
    /// Full information about an artist, returned by <see cref="ArtistClient.GetAsync"/> and <see cref="ArtistClient.GetByAliasAsync"/>.
    /// </summary>
    public class Artist : IncompleteArtist
    {
        /// <summary>
        /// Link to the cover image of the artist.
        /// </summary>
        [JsonPropertyName("cover")]
        public string Cover { get; set; }

        /// <summary>
        /// Biography of the artist.
        /// </summary>
        [JsonPropertyName("biography")]
        public string Biography { get; set; }

        /// <summary>
        /// Short biography of the artist.
        /// </summary>
        [JsonPropertyName("sortBiography")] // Typo in the API
        public string? ShortBiography { get; set; }

        /// <summary>
        /// Nationality of the artist.
        /// </summary>
        [JsonPropertyName("national")]
        public string Nationality { get; set; }

        /// <summary>
        /// Birthday of the artist.
        /// </summary>
        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }

        /// <summary>
        /// Real name of the artist.
        /// </summary>
        [JsonPropertyName("realname")]
        public string RealName { get; set; }

        /// <summary>
        /// The top album of the artist, displayed as the newest album in the artist's page.
        /// </summary>
        [JsonPropertyName("topAlbum")]
        public Album? TopAlbum { get; set; }

        /// <summary>
        /// Official Account link of the artist.
        /// </summary>
        [JsonPropertyName("oalink")]
        public string OALink { get; set; }

        /// <summary>
        /// Official Account ID of the artist.
        /// </summary>
        [JsonPropertyName("oaid")]
        public int OAID { get; set; }

        /// <inheritdoc cref="IsOA"/>
        [JsonPropertyName("hasOA")]
        public bool HasOA { get; set; }

        /// <inheritdoc cref="IncompleteArtist.TotalFollow"/>
        [JsonPropertyName("follow")]
        public long Follows { get; set; }

        /// <summary>
        /// List of awards the artist has received.
        /// </summary>
        [JsonPropertyName("awards")]
        public List<string>? Awards { get; set; }

        /// <summary>
        /// List of sections in the artist's page.
        /// </summary>
        [JsonPropertyName("sections")]
        public List<ArtistSection> Sections { get; set; }

#pragma warning disable CS1591 // Unknown JSON properties
        [JsonPropertyName("sectionId")]
        public string SectionId { get; set; }
        [JsonPropertyName("tabs")]
        public int[] Tabs { get; set; }
#pragma warning restore CS1591

        public List<Album> GetSingleAndEPs()
        {
            List<Album> albums = new List<Album>();
            foreach (var section in Sections)
            {
                if (section.SectionType == "aSingle")
                    albums.AddRange(section.Items.ConvertAll(item => (Album)item));
            }
            return albums;
        }

        public List<Album> GetAlbums()
        {
            List<Album> albums = new List<Album>();
            foreach (var section in Sections)
            {
                if (section.SectionType == "aAlbum")
                    albums.AddRange(section.Items.ConvertAll(item => (Album)item));
            }
            return albums;
        }

        public List<Album> GetAllAlbums() => GetSingleAndEPs().Concat(GetAlbums()).ToList();

        public List<Song> GetPopularSongs()
        {
            List<Song> songs = new List<Song>();
            foreach (var section in Sections)
            {
                if (section.SectionType == "aSongs")
                    songs.AddRange(section.Items.ConvertAll(item => (Song)item));
            }
            return songs;
        }

        public List<IncompleteVideo> GetMVs()
        {
            List<IncompleteVideo> videos = new List<IncompleteVideo>();
            foreach (var section in Sections)
            {
                if (section.SectionType == "aMV")
                    videos.AddRange(section.Items.ConvertAll(item => (IncompleteVideo)item));
            }
            return videos;
        }

        public List<Playlist> GetPlaylists()
        {
            List<Playlist> playlists = new List<Playlist>();
            foreach (var section in Sections)
            {
                if (section.SectionType == "aPlaylist")
                    playlists.AddRange(section.Items.ConvertAll(item => (Playlist)item));
            }
            return playlists;
        }

        public List<IncompleteArtist> GetRelatedArtists()
        {
            List<IncompleteArtist> artists = new List<IncompleteArtist>();
            foreach (var section in Sections)
            {
                if (section.SectionType == "aReArtist")
                    artists.AddRange(section.Items.ConvertAll(item => (IncompleteArtist)item));
            }
            return artists;
        }
    }
}
