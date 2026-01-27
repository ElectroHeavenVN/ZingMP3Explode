using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
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

#if !NET7_0_OR_GREATER
        internal static async Task<string> ReadAsStringAsync(this HttpContent content, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            string result = await content.ReadAsStringAsync();
            return result;
        }

        internal static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
#endif

        internal static string GetStringValue(this JsonNode? jsonNode, string? defaultValue = "") => jsonNode?.GetValue<string>() ?? defaultValue ?? string.Empty;

        internal static int GetIntValue(this JsonNode? jsonNode, int defaultValue = 0) => jsonNode?.GetValue<int?>() ?? defaultValue;

        internal static long GetLongValue(this JsonNode? jsonNode, long defaultValue = 0) => jsonNode?.GetValue<long>() ?? defaultValue;

        internal static double GetDoubleValue(this JsonNode? jsonNode, double defaultValue = 0) => jsonNode?.GetValue<double>() ?? defaultValue;

        internal static bool GetBoolValue(this JsonNode? jsonNode, bool defaultValue = false) => jsonNode?.GetValue<bool>() ?? defaultValue;
    }
}