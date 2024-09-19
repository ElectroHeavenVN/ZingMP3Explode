using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Artists;
using ZingMP3Explode.Enums;
using ZingMP3Explode.Exceptions;

public static class ExtensionMethods
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

#if !NET6_0_OR_GREATER
    internal static CookieCollection GetAllCookies(this CookieContainer container)
    {
        Hashtable? m_domainTable = (Hashtable?)container.GetType().GetField(nameof(m_domainTable), BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(container);
        Type PathList = typeof(CookieContainer).Assembly.GetType("System.Net.PathList");
        CookieCollection cookieCollection = new CookieCollection();
        lock (m_domainTable.SyncRoot)
        {
            foreach (DictionaryEntry entry in m_domainTable)
            {
                SortedList m_list = (SortedList)PathList.GetField("m_list", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(entry.Value);
                lock (m_list.SyncRoot)
                {
                    foreach (DictionaryEntry item in m_list)
                    {
                        foreach (Cookie cookie in (CookieCollection)item.Value)
                            cookieCollection.Add(cookie);
                    }
                }
            }
            return cookieCollection;
        }
    }
#endif
}

internal static class HttpExtensions
{
    public static async ValueTask<HttpResponseMessage> HeadAsync(this HttpClient http, string requestUri, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Head, requestUri);
        return await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
    }

    public static async ValueTask<Stream> GetStreamAsync(this HttpClient http, string requestUri, long? from = null, long? to = null, bool ensureSuccess = true, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Range = new RangeHeaderValue(from, to);
        var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (ensureSuccess)
            response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStreamAsync(cancellationToken);
    }

    public static async ValueTask<long?> TryGetContentLengthAsync(this HttpClient http, string requestUri, bool ensureSuccess = true, CancellationToken cancellationToken = default)
    {
        using var response = await http.HeadAsync(requestUri, cancellationToken);
        if (ensureSuccess)
            response.EnsureSuccessStatusCode();
        return response.Content.Headers.ContentLength;
    }

    public static async ValueTask<string> GetAsync(this HttpClient http, string url, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await http.ExecuteAsync(request, cancellationToken);
    }

    public static async ValueTask<string> PostAsync(this HttpClient http, string url, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        return await http.ExecuteAsync(request, cancellationToken);
    }

    public static async ValueTask<string> PostAsync(this HttpClient http, string url, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        for (var j = 0; j < headers.Count; j++)
            request.Headers.TryAddWithoutValidation(headers.ElementAt(j).Key, headers.ElementAt(j).Value);
        return await http.ExecuteAsync(request, cancellationToken);
    }

    public static async ValueTask<string> PostAsync(this HttpClient http, string url, IDictionary<string, string>? headers, HttpContent content, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        for (int i = 0; i < headers?.Count; i++)
            request.Headers.TryAddWithoutValidation(headers.ElementAt(i).Key, headers.ElementAt(i).Value);
        request.Content = content;
        return await http.ExecuteAsync(request, cancellationToken);
    }

    public static async ValueTask<long> GetFileSizeAsync(this HttpClient http, string url, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Head, url);
        for (var j = 0; j < headers.Count; j++)
            request.Headers.TryAddWithoutValidation(headers.ElementAt(j).Key, headers.ElementAt(j).Value);
        using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (response.IsSuccessStatusCode)
            return response.Content.Headers.ContentLength ?? 0;
        string nl = Environment.NewLine;
        throw new HttpRequestException($"Response status code does not indicate success: {(int)response.StatusCode} ({response.StatusCode}).{nl}Request:{nl}{request}");
    }

    public static async ValueTask<string> ExecuteAsync(this HttpClient http, string url, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await http.ExecuteAsync(request, cancellationToken);
    }

    public static async ValueTask<string> ExecuteAsync(this HttpClient http, string url, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        for (var j = 0; j < headers.Count; j++)
            request.Headers.TryAddWithoutValidation(headers.ElementAt(j).Key, headers.ElementAt(j).Value);
        return await http.ExecuteAsync(request, cancellationToken);
    }

    public static async Task<string> ExecuteAsync(this HttpClient http, HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return string.Empty;
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync(cancellationToken);
        string nl = Environment.NewLine;
        throw new HttpRequestException($"Response status code does not indicate success: {(int)response.StatusCode} ({response.StatusCode}).{nl}Request:{nl}{request}");
    }
}