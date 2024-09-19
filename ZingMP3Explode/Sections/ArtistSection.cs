using System.Text.Json.Serialization;
using ZingMP3Explode.Converters;

namespace ZingMP3Explode.Sections
{
    [JsonConverter(typeof(ArtistSectionJSONConverter))]
    public class ArtistSection : Section
	{

	}
}