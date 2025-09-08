using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about a composer.</para>
    /// <para xml:lang="vi">Thông tin về một nhạc sĩ.</para>
    /// </summary>
    public class Composer : IncompleteArtist
    {
        [JsonConstructor]
        internal Composer() { }
    }
}
