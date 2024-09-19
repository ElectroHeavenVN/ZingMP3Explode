using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Bridge;
using ZingMP3Explode.Enums;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Songs
{
    public class SongClient
    {
        ZingMP3Endpoint endpoint;
        static readonly Regex urlRegex = new Regex(@"zingmp3\.vn\/bai-hat\/(.*?)\/(Z[A-Z0-9]{7})\.html", RegexOptions.Compiled);
        static readonly Regex shortUrlRegex = new Regex(@"zingmp3\.vn\/bai-hat\/(Z[A-Z0-9]{7})\.html", RegexOptions.Compiled);
        static readonly Regex idRegex = new Regex(@"^Z[A-Z0-9]{7}$", RegexOptions.Compiled);

        public SongClient(ZingMP3Endpoint endpoint)
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

        public async ValueTask<Song?> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid song url");
            return await GetByIdAsync(id, cancellationToken);
        }

        public async ValueTask<Song?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!idRegex.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid song id");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", id }
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/page/get/song", parameters, cancellationToken);
            Utils.CheckErrorCode(resolvedJson, out JsonNode node);
            return node.Deserialize<Song>(JsonDefaults.Options);
        }

        public async ValueTask<string> GetLinkAsync(string id, CancellationToken cancellationToken = default)
        {
            Song? song = await GetByIdAsync(id, cancellationToken);
            if (song == null)
                throw new ZingMP3ExplodeException("Song not found");
            return Constants.ZINGMP3_LINK.TrimEnd('/') + song.Link;
        }

        public async ValueTask<string> GetAudioStreamLinkByUrlAsync(string url, AudioQuality quality = AudioQuality.Best, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid song url");
            return await GetAudioStreamLinkAsync(id, quality, cancellationToken);
        }

        public async ValueTask<string> GetAudioStreamLinkAsync(string id, AudioQuality quality = AudioQuality.Best, CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", id }
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/song/get/streaming", parameters, cancellationToken);
            Utils.CheckErrorCode(resolvedJson, out JsonNode node);

            string normalQuality = "";
            string highQuality = "";
            string losslessQuality = "";
            if (node.AsObject().ContainsKey("128"))
                normalQuality = node["128"].GetValue<string>();
            if (node.AsObject().ContainsKey("320"))
                highQuality = node["320"].GetValue<string>();
            if (node.AsObject().ContainsKey("lossless"))
                losslessQuality = node["lossless"].GetValue<string>();
            if (quality == AudioQuality.Best)
            {
                if (!string.IsNullOrEmpty(losslessQuality))
                    return losslessQuality;
                if (!string.IsNullOrEmpty(highQuality) && highQuality != "VIP")
                    return highQuality;
                if (!string.IsNullOrEmpty(normalQuality))
                    return normalQuality;
                throw new ZingMP3ExplodeException("The song has no stream link.");
            }
            if (quality == AudioQuality.Lossless)
                return losslessQuality;
            if (quality == AudioQuality.High)
                return highQuality;
            if (quality == AudioQuality.Normal)
                return normalQuality;
            throw new ZingMP3ExplodeException("Invalid audio quality", new ArgumentOutOfRangeException(nameof(quality)));
        }

        public async Task<Dictionary<AudioQuality, string>> GetAllAudioStreamLinksByUrlAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid song url");
            return await GetAllAudioStreamLinksAsync(id, cancellationToken);
        }

        public async Task<Dictionary<AudioQuality, string>> GetAllAudioStreamLinksAsync(string id, CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", id }
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/song/get/streaming", parameters, cancellationToken);
            Utils.CheckErrorCode(resolvedJson, out JsonNode node);
            Dictionary<AudioQuality, string> result = new Dictionary<AudioQuality, string>();
            if (node.AsObject().ContainsKey("128"))
                result.Add(AudioQuality.Normal, node["128"].GetValue<string>());
            if (node.AsObject().ContainsKey("320"))
                result.Add(AudioQuality.High, node["320"].GetValue<string>());
            if (node.AsObject().ContainsKey("lossless"))
                result.Add(AudioQuality.Lossless, node["lossless"].GetValue<string>());
            return result;
        }

        public async ValueTask<LyricData?> GetLyricsAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!idRegex.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid song id");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "id", id }
            };
            var resolvedJson = await endpoint.GetAsync("/api/v2/lyric/get/lyric", parameters, cancellationToken);
            Utils.CheckErrorCode(resolvedJson, out JsonNode node);
            LyricData? lyricData = node.Deserialize<LyricData>(JsonDefaults.Options);
            if (lyricData.File != null)
            {
                HttpResponseMessage message = await endpoint.Client.GetAsync(lyricData.File, cancellationToken);
                if (message.IsSuccessStatusCode)
                    lyricData.SyncedLyrics = await message.Content.ReadAsStringAsync(cancellationToken);
            }
            return lyricData;
        }

        public async ValueTask<LyricData?> GetLyricsByUrlAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!IsUrlValid(url, out string id))
                throw new ZingMP3ExplodeException("Invalid song url");
            return await GetLyricsAsync(id, cancellationToken);
        }

        // /api/v2/playlist/get/section-bottom
    }
}
