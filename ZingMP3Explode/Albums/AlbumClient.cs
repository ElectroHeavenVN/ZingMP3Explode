using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Bridge;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Albums
{
    public class AlbumClient
    {
        ZingMP3Endpoint endpoint;
        static readonly Regex idRegex = new Regex(@"^[A-Z0-9]{8}$", RegexOptions.Compiled);
        static readonly Regex urlRegex = new Regex(@"zingmp3\.vn\/album\/(.*?)\/([A-Z0-9]{8})\.html", RegexOptions.Compiled);
        static readonly Regex shortUrlRegex = new Regex(@"zingmp3\.vn\/album\/([A-Z0-9]{8})\.html", RegexOptions.Compiled);

        public AlbumClient(ZingMP3Endpoint endpoint)
        {
            this.endpoint = endpoint;
        }

        public bool IsUrlValid(string url) => IsUrlValid(url, out _);

        bool IsUrlValid(string url, out string id)
        {
            id = "";
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;
            Match match = urlRegex.Match(url);
            if (match.Success)
            {
                id = match.Groups[2].Value;
                return true;
            }
            match = shortUrlRegex.Match(url);
            if (match.Success)
            {
                id = match.Groups[1].Value;
                return true;
            }
            return false;
        }

        public async ValueTask<Album?> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid album url");
            return await GetByIdAsync(id, cancellationToken);
        }

        public async ValueTask<Album?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!idRegex.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid album id");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", id },
                { "thumbSize", "600_600" }
            };
            string resolvedJson = await endpoint.GetAsync("/api/v2/page/get/playlist", parameters, cancellationToken);
            Utils.CheckErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<Album>(JsonDefaults.Options);
        }
    }
}
