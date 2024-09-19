using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using ZingMP3Explode.Cookies;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode
{
    internal static class Http
    {
        internal static HttpClient GetClientWithCookie(List<ZingMP3Cookie> cookies, out CookieContainer cookieContainer)
        {
            cookieContainer = new CookieContainer();
            foreach (ZingMP3Cookie cookie in cookies)
                cookieContainer.Add(cookie.ToCookie());
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = true,
                CookieContainer = cookieContainer,
            };
            HttpClient httpClient = new HttpClient(handler, true);
            httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
            httpClient.DefaultRequestHeaders.Accept.ParseAdd("*/*");
            httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("vi-VN,vi;q=0.9");
            return httpClient;
        }

        internal static HttpClient GetClientWithCookie(string cookies, out CookieContainer cookieContainer)
        {
            cookieContainer = new CookieContainer();
            cookies = cookies.Replace("Cookie: ", "");
            string[] array = cookies.Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                if (!array[i].Contains("="))
                    continue;
                string[] cookie = array[i].Trim().Split('=');
                try
                {
                    cookieContainer.Add(new Cookie(cookie[0], cookie[1]));
                }
                catch { }
            }
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = true,
                CookieContainer = cookieContainer,
            };
            HttpClient httpClient = new HttpClient(handler, true);
            httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
            httpClient.DefaultRequestHeaders.Accept.ParseAdd("*/*");
            httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("vi-VN,vi;q=0.9");
            return httpClient;
        }

        internal static HttpClient GetClient(out CookieContainer cookieContainer)
        {
            cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = true,
                CookieContainer = cookieContainer,
            };
            HttpClient httpClient = new HttpClient(handler, true);
            httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
            httpClient.DefaultRequestHeaders.Accept.ParseAdd("*/*");
            httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("vi-VN,vi;q=0.9");
            return httpClient;
        }

        #region UA
        static string ChromeUserAgent()
        {
            var major = Randomizer.Instance.Next(100, 200);
            var build = Randomizer.Instance.Next(4000);
            var branchBuild = Randomizer.Instance.Next(200);

            return $"Mozilla/5.0 ({RandomWindowsVersion()}) AppleWebKit/537.36 (KHTML, like Gecko) " +
                $"Chrome/{major}.0.{build}.{branchBuild} Safari/537.36";
        }

        static string FirefoxUserAgent()
        {
            int version = Randomizer.Instance.Next(80, 150);
            return $"Mozilla/5.0 ({RandomWindowsVersion()}; rv:{version}.0) Gecko/20100101 Firefox/{version}.0";
        }

        static string MicrosoftEdgeUserAgent()
        {
            var major = Randomizer.Instance.Next(100, 200);
            var build = Randomizer.Instance.Next(4000);
            var branchBuild = Randomizer.Instance.Next(200);
            return $"Mozilla/5.0 ({RandomWindowsVersion()}) AppleWebKit/537.36 (KHTML, like Gecko) " +
                $"Chrome/{major}.0.{build}.{branchBuild} Safari/537.36 Edg/{major}.0.{build}.{branchBuild}";
        }

        static string ZingMP3ClientUserAgent()
        {
            return "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) zmp3/1.1.7 Chrome/102.0.5005.167 Electron/19.1.9 Safari/537.36";
        }

        internal static string RandomUserAgent()
        {
            var rand = Randomizer.Instance.Next(99) + 1;
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
            var random = Randomizer.Instance.Next(99) + 1;

            // Windows 10 = 45% popularity
            if (random >= 1 && random <= 45)
                windowsVersion += "10.0";

            // Windows 7 = 35% popularity
            else if (random > 45 && random <= 80)
                windowsVersion += "6.1";

            // Windows 8.1 = 15% popularity
            else if (random > 80 && random <= 95)
                windowsVersion += "6.3";

            // Windows 8 = 5% popularity
            else
                windowsVersion += "6.2";

            // Append WOW64 for X64 system
            if (Randomizer.Instance.NextDouble() <= 0.65)
                windowsVersion += Randomizer.Instance.NextDouble() <= 0.5 ? "; WOW64" : "; Win64; x64";

            return windowsVersion;
        }
    }
}