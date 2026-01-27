using System;
using System.Text.Json.Nodes;
using ZingMP3Explode.Entities;
using ZingMP3Explode.Exceptions;

namespace ZingMP3Explode.Utilities
{
    internal static class ExtensionMethods
    {
        internal static string GetName(this SortType type)
        {
            return type switch
            {
                SortType.Popular => "listen",
                SortType.Newest => "new",
                _ => throw new ZingMP3ExplodeException("Invalid sort type", new ArgumentOutOfRangeException(nameof(type)))
            };
        }

        internal static string GetName(this SearchFilter filter)
        {
            return filter switch
            {
                SearchFilter.Song => "song",
                SearchFilter.PlaylistAndAlbums => "playlist",
                SearchFilter.Artist => "artist",
                SearchFilter.Video => "video",
                _ => throw new ZingMP3ExplodeException("Invalid search filter", new ArgumentOutOfRangeException(nameof(filter)))
            };
        }

        internal static string GetStringValue(this JsonNode? jsonNode, string? defaultValue = "") => jsonNode?.GetValue<string>() ?? defaultValue ?? string.Empty;

        internal static int GetIntValue(this JsonNode? jsonNode, int defaultValue = 0) => jsonNode?.GetValue<int?>() ?? defaultValue;

        internal static long GetLongValue(this JsonNode? jsonNode, long defaultValue = 0) => jsonNode?.GetValue<long>() ?? defaultValue;

        internal static double GetDoubleValue(this JsonNode? jsonNode, double defaultValue = 0) => jsonNode?.GetValue<double>() ?? defaultValue;

        internal static bool GetBoolValue(this JsonNode? jsonNode, bool defaultValue = false) => jsonNode?.GetValue<bool>() ?? defaultValue;
    }
}