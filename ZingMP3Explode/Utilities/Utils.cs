using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using ZingMP3Explode.Exceptions;

namespace ZingMP3Explode.Utilities
{
    internal class Utils
    {
        internal static string HashSHA512(string text, string secretKey) => BitConverter.ToString(new HMACSHA512(Encoding.UTF8.GetBytes(secretKey)).ComputeHash(Encoding.UTF8.GetBytes(text)), 0).Replace("-", "").ToLower();

        internal static string HashSHA256(string text) => BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(text)), 0).Replace("-", "").ToLower();

        internal static void CheckZingErrorCode(string json, out JsonNode node)
        {
            node = JsonNode.Parse(json);
            int errorCode = node["err"].GetValue<int>();
            if (errorCode != 0)
                throw new ZingMP3ExplodeException(errorCode, node["msg"].GetValue<string>());
            if (!node.AsObject().ContainsKey("data"))
                throw new ZingMP3ExplodeException("The API does not return any data");
            node = node["data"];
        }

        internal static void CheckZaloErrorCode(string json, out JsonNode node)
        {
            node = JsonNode.Parse(json);
            int errorCode = node["error_code"].GetValue<int>();
            if (errorCode != 0)
                throw new ZingMP3ExplodeException(errorCode, node["error_message"].GetValue<string>());
            if (!node.AsObject().ContainsKey("data"))
                throw new ZingMP3ExplodeException("The API does not return any data");
            node = node["data"];
        }
    }
}
