using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ZingMP3Explode.Entities;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.SourceGen
{
    internal class ArtistConverter : JsonConverter<Artist>
    {
        public override Artist Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonSerializerOptions op = new JsonSerializerOptions(options);
            op.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            op.Converters.Remove(this);
            SourceGenerationContext context = new SourceGenerationContext(op);
            JsonNode? jsonNode = JsonNode.Parse(ref reader);
            Artist? artist = jsonNode?.Deserialize(context.Artist);
            if (artist is null || jsonNode is null)
                throw new JsonException("Failed to deserialize Artist object.");
            if (jsonNode.AsObject().ContainsKey("sections"))
            {
                foreach (var section in jsonNode["sections"]!.AsArray())
                {
                    if (section is null || section["sectionType"] is null)
                        continue;
                    string id = section["sectionId"].GetStringValue();
                    switch (id)
                    {
                        case "aSongs":
                            var pageSectionSong = section.Deserialize(context.PageSectionSong)!;
                            artist.sections.Add(pageSectionSong.Clone());
                            break;
                        case "aReArtist":
                            var pageSectionArtist = section.Deserialize(context.PageSectionArtist)!;
                            artist.sections.Add(pageSectionArtist.Clone());
                            break;
                        case "aAlbum":
                            var pageSectionAlbum = section.Deserialize(context.PageSectionAlbum)!;
                            artist.sections.Add(pageSectionAlbum.Clone());
                            break;
                        case "aSingle":
                        case "aPlaylist":
                            var pageSectionPlaylist = section.Deserialize(context.PageSectionPlaylist)!;
                            artist.sections.Add(pageSectionPlaylist.Clone());
                            break;
                        case "aMV":
                            var pageSectionVideo = section.Deserialize(context.PageSectionVideo)!;
                            artist.sections.Add(pageSectionVideo.Clone());
                            break;
                    }
                }
            }
            return artist;
        }

        public override void Write(Utf8JsonWriter writer, Artist value, JsonSerializerOptions options)
        {
            SourceGenerationContext context = new SourceGenerationContext(new JsonSerializerOptions(options));
            JsonSerializer.Serialize(writer, value, context.Artist);
        }
    }
}
