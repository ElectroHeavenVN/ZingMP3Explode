using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Interfaces;
using ZingMP3Explode.Playlists;

namespace ZingMP3Explode.Albums
{
    /// <summary>
    /// Information of an album.
    /// </summary>
    public class Album : BasePlaylist
    {
        [JsonConstructor]
        public Album() { }

        public Album(BasePlaylist baseList)
        {
            if (!baseList.IsAlbum)
                throw new ZingMP3ExplodeException("The base playlist is not an album.", new ArgumentException("The base playlist is not an album.", nameof(baseList)));
            Array.ForEach(baseList.GetType().GetProperties(), property =>
            {
                if (property.CanWrite)
                    property.SetValue(this, property.GetValue(baseList));
            });
            releaseDateUnix = baseList.releaseDateUnix;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"Album ({Title})";
    }
}
