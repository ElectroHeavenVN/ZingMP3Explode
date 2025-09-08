using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using ZingMP3Explode.Exceptions;

namespace ZingMP3Explode.Utilities
{
    internal static class Utils
    {
#if NETFRAMEWORK
        static Random random = new Random();
#else 
        static Random random = Random.Shared;
#endif

        #region UA
        static string ChromeUserAgent()
        {
            var major = random.Next(150, 250);
            var build = random.Next(4000);
            var branchBuild = random.Next(200);

            return $"Mozilla/5.0 ({RandomWindowsVersion()}) AppleWebKit/537.36 (KHTML, like Gecko) " +
                $"Chrome/{major}.0.{build}.{branchBuild} Safari/537.36";
        }

        static string FirefoxUserAgent()
        {
            int version = random.Next(150, 250);
            return $"Mozilla/5.0 ({RandomWindowsVersion()}; rv:{version}.0) Gecko/20100101 Firefox/{version}.0";
        }

        static string MicrosoftEdgeUserAgent()
        {
            var major = random.Next(150, 250);
            var build = random.Next(4000);
            var branchBuild = random.Next(200);
            return $"Mozilla/5.0 ({RandomWindowsVersion()}) AppleWebKit/537.36 (KHTML, like Gecko) " +
                $"Chrome/{major}.0.{build}.{branchBuild} Safari/537.36 Edg/{major}.0.{build}.{branchBuild}";
        }

        //latest version available at: https://github.com/zmp3-pc/zmp3-pc/releases
        //WHY do they host the binary on GitHub when they clearly can afford their own CDN?
        //Also, zmp3-pc is a personal account, not an organization.
        static string ZingMP3ClientUserAgent() => "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) zmp3/1.2.0 Chrome/102.0.5005.167 Electron/19.1.9 Safari/537.36";

        internal static string RandomUserAgent()
        {
            var rand = random.Next(99) + 1;
            if (rand <= 40)
                return ChromeUserAgent();
            else if (rand <= 75)
                return MicrosoftEdgeUserAgent();
            else if (rand <= 95)
                return FirefoxUserAgent();
            return ZingMP3ClientUserAgent();
        }
        #endregion

        static string RandomWindowsVersion()
        {
            var windowsVersion = "Windows NT ";
            var num = random.Next(99) + 1;

            // Windows 10 = 45% popularity
            if (num >= 1 && num <= 45)
                windowsVersion += "10.0";

            // Windows 7 = 35% popularity
            else if (num > 45 && num <= 80)
                windowsVersion += "6.1";

            // Windows 8.1 = 15% popularity
            else if (num > 80 && num <= 95)
                windowsVersion += "6.3";

            // Windows 8 = 5% popularity
            else
                windowsVersion += "6.2";

            // Append WOW64 for X64 system
            if (random.NextDouble() <= 0.65)
                windowsVersion += random.NextDouble() <= 0.5 ? "; WOW64" : "; Win64; x64";

            return windowsVersion;
        }

        internal static string HashSHA512(string text, string secretKey) => BitConverter.ToString(new HMACSHA512(Encoding.UTF8.GetBytes(secretKey)).ComputeHash(Encoding.UTF8.GetBytes(text)), 0).Replace("-", "").ToLower();

        internal static string HashSHA256(string text)
        {
#if NETFRAMEWORK
            return BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(text)), 0).Replace("-", "").ToLower();
#else
            return BitConverter.ToString(SHA256.HashData(Encoding.UTF8.GetBytes(text)), 0).Replace("-", "").ToLower();
#endif
        }

        internal static string ChainParams(Dictionary<string, object> parameters)
        {
            string urlParam = "";
            foreach (var param in parameters)
            {
                string key = param.Key;
                string value = param.Value.ToString()!;
                urlParam += key + "=" + Uri.EscapeDataString(value) + "&";
            }
            urlParam = urlParam.Remove(urlParam.Length - 1);
            return urlParam;
        }

        internal static void CheckErrorCode(string json, out JsonNode node)
        {
            JsonNode? jsonNode = JsonNode.Parse(json) ?? throw new ZingMP3ExplodeException("The API returned an invalid response");
            int errorCode = jsonNode["err"].GetIntValue();
            if (errorCode != 0)
                throw new ZingMP3APIException(errorCode, jsonNode["msg"].GetStringValue());
            if (!jsonNode.AsObject().ContainsKey("data") || jsonNode["data"] is null)
                throw new ZingMP3ExplodeException("The API does not return any data");
            node = jsonNode["data"]!;
        }
    }
}
