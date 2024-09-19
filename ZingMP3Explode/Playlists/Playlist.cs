using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.Playlists
{
    /// <summary>
    /// Information of a playlist.
    /// </summary>
    public class Playlist : BasePlaylist
    {
        [JsonConstructor]
        public Playlist() { }

        public Playlist(BasePlaylist baseList)
        {
            Array.ForEach(baseList.GetType().GetProperties(), property =>
            {
                if (property.CanWrite)
                    property.SetValue(this, property.GetValue(baseList));
            });
            releaseDateUnix = baseList.releaseDateUnix;
        }
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"Playlist ({Title})";
    }
}
