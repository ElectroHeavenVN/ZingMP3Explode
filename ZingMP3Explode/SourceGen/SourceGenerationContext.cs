using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZingMP3Explode.Entities;

namespace ZingMP3Explode.SourceGen
{
    [JsonSerializable(typeof(Album))]
    [JsonSerializable(typeof(Artist))]
    [JsonSerializable(typeof(Composer))]
    [JsonSerializable(typeof(IncompleteArtist))]
    [JsonSerializable(typeof(Genre))]
    [JsonSerializable(typeof(IncompleteGenre))]
    [JsonSerializable(typeof(BaseList))]
    [JsonSerializable(typeof(Playlist))]

    [JsonSerializable(typeof(PageSection<Video>))]
    [JsonSerializable(typeof(PageSection<Artist>))]
    [JsonSerializable(typeof(PageSection<Song>))]
    [JsonSerializable(typeof(PageSection<Album>))]
    [JsonSerializable(typeof(PageSection<Playlist>))]
    [JsonSerializable(typeof(List<PageSection<Video>>))]
    [JsonSerializable(typeof(List<PageSection<Artist>>))]
    [JsonSerializable(typeof(List<PageSection<Song>>))]
    [JsonSerializable(typeof(List<PageSection<Album>>))]
    [JsonSerializable(typeof(List<PageSection<Playlist>>))]
    [JsonSerializable(typeof(PageSectionOption))]

    [JsonSerializable(typeof(LyricData))]
    [JsonSerializable(typeof(LyricData.Sentence))]
    [JsonSerializable(typeof(LyricData.Word))]
    [JsonSerializable(typeof(PreviewInfo))]
    [JsonSerializable(typeof(Song))]
    [JsonSerializable(typeof(List<Song>))]
    [JsonSerializable(typeof(SongList))]
    [JsonSerializable(typeof(CurrentUser))]
    [JsonSerializable(typeof(IncompleteVideo))]
    [JsonSerializable(typeof(List<IncompleteVideo>))]
    [JsonSerializable(typeof(Video))]
    [JsonSerializable(typeof(List<Video>))]
    [JsonSerializable(typeof(VideoLyrics))]
    [JsonSerializable(typeof(VideoStream))]
    [JsonSerializable(typeof(MultiSearchResult))]
    [JsonSerializable(typeof(CurrentUserAssets))]
    [JsonSerializable(typeof(ZingChartData))]
    [JsonSerializable(typeof(RealTimeChart))]
    [JsonSerializable(typeof(RealTimeChartGraph))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {
        static SourceGenerationContext()
        {
            Default = new SourceGenerationContext(CreateJsonSerializerOptions(Default));
        }

        static JsonSerializerOptions CreateJsonSerializerOptions(SourceGenerationContext defaultContext)
        {
            var options = new JsonSerializerOptions(defaultContext.GeneratedSerializerOptions!)
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
            options.Converters.Add(new ArtistConverter());
            options.Converters.Add(new ChartGraphTimesConverter());
            return options;
        }
    }
}
