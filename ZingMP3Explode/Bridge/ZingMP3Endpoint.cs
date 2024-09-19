using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Bridge
{
    public class ZingMP3Endpoint
    {
        readonly HttpClient httpClient;

        bool isGetZingMP3MainPage;
        string zingMP3Version = Constants.DEFAULT_VERSION;
        static readonly Regex mainMinJSRegex = new Regex("https://zjs.zmdcdn.me/zmp3-desktop/releases/(.*?)/static/js/main.min.js", RegexOptions.Compiled);
        static readonly string[] paramsIncludedInHash = { "ctime", "id", "type", "page", "count", "version" };

        public string APIKey { get; set; }

        public string Secret { get; set; }

        internal HttpClient Client => httpClient;

        public ZingMP3Endpoint(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async ValueTask<string> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            await CheckVersion(cancellationToken);
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            return await httpClient.ExecuteAsync(request, cancellationToken);
        }

        public async ValueTask<string> GetAsync(string apiEndpoint, Dictionary<string, string> parameters, CancellationToken cancellationToken = default)
        {
            await CheckVersion(cancellationToken);
            string[] parametersArray = parameters.Select(x => $"{x.Key}={x.Value}").ToArray();
            return await APIGet(apiEndpoint, parametersArray, cancellationToken);
        }

        async ValueTask<string> APIGet(string apiEndpoint, string[] parameters, CancellationToken cancellationToken = default)
        {
            string ctime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            List<string> listParameters = parameters.ToList();
            listParameters.Add($"ctime={ctime}");
            listParameters.Add($"version={zingMP3Version}");
            string webParameter = string.Join("&", listParameters.ToArray());
            listParameters = listParameters.Where(p => paramsIncludedInHash.Any(p2 => p.Contains(p2 + '='))).ToList();
            listParameters.Sort(string.Compare);
            string parameter = string.Join("", listParameters);
            string hash = apiEndpoint + Utils.HashSHA256(parameter);
            string sig = Utils.HashSHA512(hash, Secret);
            string apiUrl = $"{Constants.ZINGMP3_LINK.TrimEnd('/')}{apiEndpoint}?{webParameter}&sig={sig}&apiKey={APIKey}";
            return await GetAsync(apiUrl, cancellationToken);
        }

        async Task CheckVersion(CancellationToken cancellationToken)
        {
            if (isGetZingMP3MainPage)
                return;
            var response = await httpClient.GetAsync(Constants.ZINGMP3_LINK, cancellationToken);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to get ZingMP3 main page");
            string html = await response.Content.ReadAsStringAsync(cancellationToken);
            int startIndex = html.LastIndexOf("/static/js/main.min.js") - 100;
            if (startIndex >= 0)
            {
                html = html.Substring(startIndex);
                zingMP3Version = mainMinJSRegex.Match(html).Groups[1].Value.Replace("v", "");
                isGetZingMP3MainPage = true;
            }
        }
    }
}
