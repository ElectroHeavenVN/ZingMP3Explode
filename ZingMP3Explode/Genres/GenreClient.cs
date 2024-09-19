using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using ZingMP3Explode.Bridge;
using ZingMP3Explode.Utilities;
using System.Text.RegularExpressions;
using ZingMP3Explode.Exceptions;

namespace ZingMP3Explode.Genres
{
    public class GenreClient
    {
        ZingMP3Endpoint endpoint;

        static readonly Regex idRegex = new Regex(@"^I[A-Z0-9]{7}$", RegexOptions.Compiled);

        public GenreClient(ZingMP3Endpoint endpoint)
        {
            this.endpoint = endpoint;
        }

        public async ValueTask<Genre?> GetAsync(string id)
        {
            if (!idRegex.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid genre id");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", id },
                { "type", "album" }
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/genre/get/info", parameters);
            Utils.CheckZingErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<Genre>(JsonDefaults.Options);
        }
    }
}
