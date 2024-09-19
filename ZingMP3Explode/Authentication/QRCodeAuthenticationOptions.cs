using System.Text.Json.Serialization;

namespace ZingMP3Explode.Authentication
{
    public class QRCodeAuthenticationOptions
    {
        [JsonPropertyName("enabledMultiLayer")]
        public bool MultiLayerEnabled { get; set; }

        [JsonPropertyName("enabledCheckOCR")]
        public bool CheckOCREnabled { get; set; }
    }
}
