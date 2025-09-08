using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about a genre.</para>
    /// <para xml:lang="vi">Thông tin về một thể loại.</para>
    /// </summary>
    public class Genre : IncompleteGenre
    {
        [JsonConstructor]
        internal Genre() { }

        /// <summary>
        /// <para xml:lang="en">Parent genre.</para>
        /// <para xml:lang="vi">Thể loại cha.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("parent")]
        public IncompleteGenre? Parent { get; internal set; }

        [JsonInclude, JsonPropertyName("childs")]
        internal List<IncompleteGenre> childs { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">Child genres.</para>
        /// <para xml:lang="vi">Các thể loại con.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<IncompleteGenre> Childs => childs.AsReadOnly();
    }
}
