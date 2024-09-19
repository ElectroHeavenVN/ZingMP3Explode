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
using ZingMP3Explode.Sections;
using ZingMP3Explode.Converters;

namespace ZingMP3Explode.Videos
{
    public class VideoClient
    {
        ZingMP3Endpoint endpoint;
        static readonly Regex urlRegex = new Regex(@"zingmp3\.vn\/video-clip\/(.*?)\/(Z[A-Z0-9]{7})\.html", RegexOptions.Compiled);
        static readonly Regex idRegex = new Regex(@"^Z[A-Z0-9]{7}$", RegexOptions.Compiled);

        public VideoClient(ZingMP3Endpoint endpoint)
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
            return false;
        }

        public async ValueTask<Video?> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid video url");
            return await GetByIdAsync(id, cancellationToken);
        }

        public async ValueTask<Video?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!idRegex.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid video id");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", id }
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/page/get/video", parameters, cancellationToken);

            Utils.CheckErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<Video>(JsonDefaults.Options);
        }

        public async ValueTask<Section?> GetRelatedSectionAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid video url");
            return await GetRelatedSectionByIdAsync(id, cancellationToken);
        }

        public async ValueTask<Section?> GetRelatedSectionByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!idRegex.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid video id");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", id }
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/video/get/section-relate", parameters, cancellationToken);
            Utils.CheckErrorCode(resolvedJson, out JsonNode node);
            JsonSerializerOptions options = JsonDefaults.Options;
            options.Converters.Add(new SectionJSONConverter<IncompleteVideo>());
            return node.Deserialize<Section>(options);
        }
    }
}
