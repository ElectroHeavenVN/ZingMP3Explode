using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ZingMP3Explode.Cookies;
using ZingMP3Explode.Songs;

namespace ZingMP3Explode.Demo
{
    internal static class Program
    {
        static ZingMP3Client client;

        static async Task Main(string[] args)
        {
            Console.Title = "ZingMP3Explode Demo";
            Console.OutputEncoding = Encoding.UTF8;
            await InitializeClientAsync();
            await GetSongInformationAsync();
            await GetArtistInformationAsync();
            await GetAlbumInformationAsync();
            await GetMVInformationAsync();

            await Task.Run(Console.ReadLine);
        }

        static async Task InitializeClientAsync()
        {
            if (File.Exists("credentials.json"))
            {
                await Console.Out.WriteLineAsync("Previous credentials found, login with cookies from credentials.json...");
                List<ZingMP3Cookie> cookies = JsonNode.Parse(File.ReadAllText("credentials.json")).Deserialize<List<ZingMP3Cookie>>() ?? throw new NullReferenceException();
                client = new ZingMP3Client(cookies);
                await client.InitializeAsync();
                //rewrite credentials.json to remove unused properties
                File.WriteAllText("credentials.json", JsonSerializer.Serialize(client.GetCookies(), new JsonSerializerOptions() { WriteIndented = true }));
            }
            else
            {
                await Console.Out.WriteLineAsync("No credentials found, trying to login via QR code...");
                client = new ZingMP3Client();
                await client.InitializeAsync();
                await client.LoginWithQRCodeAsync(async qr =>
                {
                    await Console.Out.WriteLineAsync("Opening QR code...");
                    string file = Environment.ExpandEnvironmentVariables("%TEMP%") + "\\qr.png";
                    File.WriteAllBytes(file, Convert.FromBase64String(qr.Base64Image.Replace("data:image/png;base64,", "")));
                    Process.Start(file);
                },
                async user =>
                {
                    await Console.Out.WriteLineAsync("Logged in as: " + user.DisplayName + ", waiting for comfirmation...");
                });
                File.WriteAllText("credentials.json", JsonSerializer.Serialize(client.GetCookies(), new JsonSerializerOptions() { WriteIndented = true }));
            }
            var currentUser = await client.CurrentUser.GetAsync();
            await Console.Out.WriteLineAsync($"Logged in successfully! Current user: {currentUser.DisplayName}");
        }

        static async Task GetSongInformationAsync()
        {
            await Console.Out.WriteAsync($"Enter song link: ");
            string link = await Console.In.ReadLineAsync();
            var song = await client.Songs.GetAsync(link);
            await Console.Out.WriteLineAsync($"Title: {song.Title}");
            await Console.Out.WriteLineAsync($"Artists: {song.AllArtistsNames}");
            await Console.Out.WriteLineAsync($"Duration (seconds): {song.Duration}");
            await Console.Out.WriteLineAsync($"Album: {song.Album.Title}");
            if (!string.IsNullOrEmpty(song.MVLink))
                await Console.Out.WriteLineAsync($"MV: {song.MVLink}");
            string mp3Link = await client.Songs.GetAudioStreamLinkAsync(song.Id);
            await Console.Out.WriteLineAsync($"MP3 link: {mp3Link}");
            LyricData lyricData = await client.Songs.GetLyricsAsync(song.Id);
            await Console.Out.WriteLineAsync($"Synced lyrics:\r\n {lyricData.SyncedLyrics}\r\n\r\n");
            await Console.Out.WriteLineAsync($"Enhanced lyrics:\r\n {lyricData.GetEnhancedLyrics()}");
            //If you have VLC installed, you can play the audio directly by uncommenting the following line
            //Process.Start("vlc", mp3Link);
            await Console.Out.WriteLineAsync("------------------------------------");
        }

        static async Task GetArtistInformationAsync()
        {
            await Console.Out.WriteAsync($"Enter artist link: ");
            string link = await Console.In.ReadLineAsync();
            var artist = await client.Artists.GetAsync(link);
            await Console.Out.WriteLineAsync($"Artist: {artist.Name}");
            await Console.Out.WriteLineAsync($"Real name: {artist.RealName}");
            await Console.Out.WriteLineAsync($"Birthday: {artist.Birthday}");
            await Console.Out.WriteLineAsync($"Nationality: {artist.Nationality}");
            await Console.Out.WriteLineAsync($"Biography: \r\n{artist.Biography}\r\n");
            var songs = await client.Artists.GetAllSongsAsync(artist.Id);
            await Console.Out.WriteLineAsync($"Songs: {songs.Count}");
            var mvs = await client.Artists.GetAllMVsAsync(artist.Id);
            await Console.Out.WriteLineAsync($"MVs: {mvs.Count}");
            await Console.Out.WriteLineAsync("------------------------------------");
        }

        static async Task GetAlbumInformationAsync()
        {
            await Console.Out.WriteAsync($"Enter album link: ");
            string link = await Console.In.ReadLineAsync();
            var album = await client.Albums.GetAsync(link);
            await Console.Out.WriteLineAsync($"Album: {album.Title}");
            await Console.Out.WriteLineAsync($"Description: {album.Description}");
            await Console.Out.WriteLineAsync($"Artists: {album.AllArtistsNames}");
            await Console.Out.WriteLineAsync($"Release date: {album.ReleaseDate}");
            await Console.Out.WriteLineAsync($"Type: {album.Type}");
            await Console.Out.WriteLineAsync($"Songs: {album.Songs.Total}");
            await Console.Out.WriteLineAsync("------------------------------------");
        }

        static async Task GetMVInformationAsync()
        {
            await Console.Out.WriteAsync($"Enter MV link: ");
            string link = await Console.In.ReadLineAsync();
            var mv = await client.Videos.GetAsync(link);
            await Console.Out.WriteLineAsync($"MV: {mv.Title}");
            await Console.Out.WriteLineAsync($"Duration: {mv.Duration}");
            await Console.Out.WriteLineAsync($"Artists: {mv.AllArtistsNames}");
            await Console.Out.WriteLineAsync($"Song: {mv.Song.Title}");
            await Console.Out.WriteLineAsync($"Album: {mv.Album.Title}");
            await Console.Out.WriteLineAsync($"HLS Stream: {mv.VideoStream.HLS.StreamLink}");
            //If you have VLC installed, you can play the video directly by uncommenting the following line
            //Process.Start("vlc", mv.VideoStream.HLS.StreamLink);
            await Console.Out.WriteLineAsync("------------------------------------");
        }
    }
}