using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZingMP3Explode.Interfaces;

namespace ZingMP3Explode.List
{
    /// <summary>
    /// Base class for lists of <see cref="IZingMP3Object"/>.
    /// </summary>
    public class BaseList<T> where T: IZingMP3Object
    {
        /// <summary>
        /// The list of items.
        /// </summary>
        [JsonPropertyName("items")]
        public List<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// The total number of items.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// Indicate whether the current list is incomplete and there are more items.
        /// </summary>
        [JsonPropertyName("hasMore")]
        public bool HasMore { get; set; }
    }
}
