using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ZingMP3Explode.Entities;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.SourceGen
{
    internal class ChartGraphTimesConverter : JsonConverter<RealTimeChartGraph>
    {
        public override RealTimeChartGraph Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonSerializerOptions op = new JsonSerializerOptions(options);
            op.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            op.Converters.Remove(this);
            SourceGenerationContext context = new SourceGenerationContext(op);
            JsonNode? jsonNode = JsonNode.Parse(ref reader);
            RealTimeChartGraph? chartGraph = jsonNode?.Deserialize(context.RealTimeChartGraph);
            if (chartGraph is null || jsonNode is null)
                throw new JsonException("Failed to deserialize RealTimeChartGraph object.");
            if (jsonNode.AsObject().ContainsKey("times"))
            {
                foreach (var time in jsonNode["times"]!.AsArray())
                {
                    if (time is null)
                        continue;
                    string hour = time["hour"].GetStringValue();
                    chartGraph.times.Add(hour);
                }
            }
            return chartGraph;
        }

        public override void Write(Utf8JsonWriter writer, RealTimeChartGraph value, JsonSerializerOptions options)
        {
            SourceGenerationContext context = new SourceGenerationContext(new JsonSerializerOptions(options));
            JsonSerializer.Serialize(writer, value, context.Artist);
        }
    }
}
