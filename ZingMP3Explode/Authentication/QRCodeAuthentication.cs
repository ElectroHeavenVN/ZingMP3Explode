using System.Text.Json.Serialization;

namespace ZingMP3Explode.Authentication
{
    public class QRCodeAuthentication
    {
        [JsonPropertyName("image")]
        public string? Base64Image { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("options")]
        public QRCodeAuthenticationOptions? Options { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
