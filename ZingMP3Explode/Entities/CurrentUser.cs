using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">Information about the current user.</para>
    /// <para xml:lang="vi">Thông tin về người dùng hiện tại.</para>
    /// </summary>
    public class CurrentUser
    {
        [JsonConstructor]
        internal CurrentUser() { }

        /// <summary>
        /// <para xml:lang="en">ID of the current user.</para>
        /// <para xml:lang="vi">ID của người dùng hiện tại.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("id")]
        public long ID { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">ID of the current user.</para>
        /// <para xml:lang="vi">ID của người dùng hiện tại.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("userid")]
        public long UserID { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">User's username.</para>
        /// <para xml:lang="vi">Tên đăng nhập của người dùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("userName")]
        public string Username { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">User's display name.</para>
        /// <para xml:lang="vi">Tên hiển thị của người dùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("displayName")]
        public string DisplayName { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">URL to the user's avatar.</para>
        /// <para xml:lang="vi">Đường dẫn đến ảnh đại diện của người dùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("avatar")]
        public string AvatarUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Url to the big thumbnail of the user.</para>
        /// <para xml:lang="vi">Đường dẫn đến ảnh đại diện lớn của người dùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("avatarM")]
        public string BigAvatarUrl { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Unix Timestamp of the current session.</para>
        /// <para xml:lang="vi">Dấu thời gian Unix của phiên hiện tại.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("time")]
        public long Timestamp { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Indicates if the user's IP address is from Vietnam.</para>
        /// <para xml:lang="vi">Cho biết địa chỉ IP của người dùng có từ Việt Nam hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("ipvn")]
        public bool IsIPAddressFromVietnam { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Indicates if the user is from Vietnam.</para>
        /// <para xml:lang="vi">Cho biết người dùng có từ Việt Nam hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("userVN")]
        public bool IsUserFromVietnam { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">User's IP address.</para>
        /// <para xml:lang="vi">Địa chỉ IP của người dùng.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("ip")]
        public string IPAddress { get; internal set; } = "";

        /// <summary>
        /// <para xml:lang="en">Indicates if the user registered with a phone number.</para>
        /// <para xml:lang="vi">Cho biết người dùng đã đăng ký bằng số điện thoại hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("phone")]
        public bool RegisteredWithPhoneNumber { get; internal set; }

        /// <summary>
        /// <para xml:lang="en">Indicates if the user's phone number is from Vietnam.</para>
        /// <para xml:lang="vi">Cho biết số điện thoại của người dùng có từ Việt Nam hay không.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName("isPhoneVN")]
        public bool IsPhoneNumberFromVietnam { get; internal set; }

        [JsonInclude, JsonPropertyName("segments")]
        internal List<string> segments { get; set; } = [];
        /// <summary>
        /// <para xml:lang="en">User's segments.</para>
        /// <para xml:lang="vi">Các phân đoạn của người dùng.</para>
        /// </summary>
        [JsonIgnore]
        public ReadOnlyCollection<string> Segments => segments.AsReadOnly();

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        [JsonInclude, JsonPropertyName("isRestrictPartner")]
        public bool IsRestrictPartner { get; internal set; }
    }
}
