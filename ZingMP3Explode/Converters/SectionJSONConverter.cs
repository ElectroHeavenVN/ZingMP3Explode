using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZingMP3Explode.Albums;
using ZingMP3Explode.Interfaces;
using ZingMP3Explode.Sections;

namespace ZingMP3Explode.Converters
{
    internal class SectionJSONConverter<T> : JsonConverter<Section> where T: IZingMP3Object
    {
        public override Section Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var section = new Section();
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                section.SectionType = root.GetProperty(typeof(Section).GetProperty(nameof(section.SectionType)).GetCustomAttribute<JsonPropertyNameAttribute>().Name).GetString() ?? "";
                section.ViewType = root.GetProperty(typeof(Section).GetProperty(nameof(section.ViewType)).GetCustomAttribute<JsonPropertyNameAttribute>().Name).GetString() ?? "";
                section.Title = root.GetProperty(typeof(Section).GetProperty(nameof(section.Title)).GetCustomAttribute<JsonPropertyNameAttribute>().Name).GetString() ?? "";
                section.Link = root.GetProperty(typeof(Section).GetProperty(nameof(section.Link)).GetCustomAttribute<JsonPropertyNameAttribute>().Name).GetString() ?? "";
                section.SectionId = root.GetProperty(typeof(Section).GetProperty(nameof(section.SectionId)).GetCustomAttribute<JsonPropertyNameAttribute>().Name).GetString() ?? "";
                if (root.TryGetProperty(typeof(Section).GetProperty(nameof(section.ItemType)).GetCustomAttribute<JsonPropertyNameAttribute>().Name, out var itemTypeElement))
                    section.ItemType = itemTypeElement.GetString();
                if (root.TryGetProperty(typeof(Section).GetProperty(nameof(section.Options)).GetCustomAttribute<JsonPropertyNameAttribute>().Name, out var optionsElement))
                    section.Options = optionsElement.Deserialize<Section.SectionOption>(options);
                if (root.TryGetProperty(typeof(Section).GetProperty(nameof(section.TopAlbum)).GetCustomAttribute<JsonPropertyNameAttribute>().Name, out var topAlbumElement))
                    section.TopAlbum = topAlbumElement.Deserialize<Album>(options);

                var items = root.GetProperty(typeof(Section).GetProperty(nameof(section.Items)).GetCustomAttribute<JsonPropertyNameAttribute>().Name).EnumerateArray();
                section.Items = new List<IZingMP3Object>();
                foreach (var item in items)
                {
                    IZingMP3Object? deserializedItem = item.Deserialize<T>(options);
                    if (deserializedItem != null)
                        section.Items.Add(deserializedItem);
                }
            }

            return section;
        }

        public override void Write(Utf8JsonWriter writer, Section value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString(typeof(Section).GetProperty(nameof(value.SectionType)).GetCustomAttribute<JsonPropertyNameAttribute>().Name, value.SectionType);
            writer.WriteString(typeof(Section).GetProperty(nameof(value.ViewType)).GetCustomAttribute<JsonPropertyNameAttribute>().Name, value.ViewType);
            writer.WriteString(typeof(Section).GetProperty(nameof(value.Title)).GetCustomAttribute<JsonPropertyNameAttribute>().Name, value.Title);
            writer.WriteString(typeof(Section).GetProperty(nameof(value.Link)).GetCustomAttribute<JsonPropertyNameAttribute>().Name, value.Link);
            writer.WriteString(typeof(Section).GetProperty(nameof(value.SectionId)).GetCustomAttribute<JsonPropertyNameAttribute>().Name, value.SectionId);
            writer.WriteString(typeof(Section).GetProperty(nameof(value.ItemType)).GetCustomAttribute<JsonPropertyNameAttribute>().Name, value.ItemType);

            writer.WritePropertyName(typeof(Section).GetProperty(nameof(value.Items)).GetCustomAttribute<JsonPropertyNameAttribute>().Name);
            writer.WriteStartArray();
            foreach (var item in value.Items)
                JsonSerializer.Serialize(writer, item, item.GetType(), options);
            writer.WriteEndArray();

            if (value.TopAlbum != null)
            {
                writer.WritePropertyName(typeof(Section).GetProperty(nameof(value.TopAlbum)).GetCustomAttribute<JsonPropertyNameAttribute>().Name);
                JsonSerializer.Serialize(writer, value.TopAlbum, options);
            }
            if (value.Options != null)
            {
                writer.WritePropertyName(typeof(Section).GetProperty(nameof(value.Options)).GetCustomAttribute<JsonPropertyNameAttribute>().Name);
                JsonSerializer.Serialize(writer, value.Options, options);
            }

            writer.WriteEndObject();
        }
    }
}
