using System.Text.Json.Serialization;

namespace ZingMP3Explode.Authentication
{
    public class QRCodeAuthenticationUser
    {
        [JsonPropertyName("avatar")]
        public string AvatarLink { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
    }
}
