using System.Net;
using System.Text.Json.Serialization;

namespace ZingMP3Explode.Cookies
{
    public class ZingMP3Cookie
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        public ZingMP3Cookie()
        {
            Name = "";
        }

        public ZingMP3Cookie(string name, string? value, string? path, string? domain)
        {
            Name = name;
            Value = value;
            Path = path;
            Domain = domain;
        }

        public Cookie ToCookie() => new Cookie(Name, Value ?? "", Path ?? "/", Domain ?? "");

        public static ZingMP3Cookie FromCookie(Cookie cookie) => new ZingMP3Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
    }
}
