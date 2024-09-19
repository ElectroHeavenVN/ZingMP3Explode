using System.Text.Json.Serialization;

namespace ZingMP3Explode.User
{
    public class LoggedInUser
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("userid")]
        public long UserId { get; set; }

        [JsonPropertyName("userName")]
        public string Username { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        /// <summary>
        /// Link to the big thumbnail of the user.
        /// </summary>
        [JsonPropertyName("avatarM")]
        public string AvatarM { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("segments")]
        public string[] Segments { get; set; }

        [JsonPropertyName("ipvn")]
        public bool IsIPAddressFromVietnam { get; set; }

        [JsonPropertyName("ip")]
        public string IPAddress { get; set; }

        [JsonPropertyName("phone")]
        public bool RegisteredWithPhoneNumber { get; set; }

        [JsonPropertyName("isPhoneVN")]
        public bool IsPhoneNumberFromVietnam { get; set; }

    }
}
