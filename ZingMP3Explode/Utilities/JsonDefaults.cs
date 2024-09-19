using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Utilities
{
    internal static class JsonDefaults
    {
        public static JsonSerializerOptions Options => GetOptions();

        static JsonSerializerOptions GetOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
            //options.Converters.Add(new CommonIdJsonConverter());
            //options.Converters.Add(new SongIdJsonConverter());
            //options.Converters.Add(new PlaylistIdJsonConverter());
            //options.Converters.Add(new AlbumIdJsonConverter());
            //options.Converters.Add(new ArtistIdJsonConverter());
            //options.Converters.Add(new UserIdJsonConverter());

            return options;
        }
    }
}
