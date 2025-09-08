using System.Text;
using ZingMP3Explode.Entities;

namespace ZingMP3Explode.Demo
{
    internal static class Program
    {
        static ZingMP3Client client;

        static async Task Main(string[] args)
        {
            Console.Title = "ZingMP3Explode Demo";
            Console.OutputEncoding = Encoding.UTF8;
            client = new ZingMP3Client();
            await client.InitializeAsync();
            await GetSongInformationAsync();
            await GetArtistInformationAsync();
            await GetAlbumInformationAsync();
            await GetMVInformationAsync();
            Console.ReadLine();
        }

        static async Task GetSongInformationAsync()
        {
            await Console.Out.WriteAsync($"Enter song link: ");
            string link = await Console.In.ReadLineAsync() ?? "";
            var song = await client.Songs.GetAsync(link);
            await Console.Out.WriteLineAsync($"Title: {song.Title}");
            await Console.Out.WriteLineAsync($"Artists: {song.AllArtistsNames}");
            await Console.Out.WriteLineAsync($"Duration (seconds): {song.Duration}");
            await Console.Out.WriteLineAsync($"Album: {song.Album?.Title}");
            if (!string.IsNullOrEmpty(song.MVUrl))
                await Console.Out.WriteLineAsync($"MV: {song.MVUrl}");
            string mp3Url = await client.Songs.GetAudioStreamUrlAsync(song.ID);
            await Console.Out.WriteLineAsync($"MP3 link: {mp3Url}");
            LyricData lyricData = await client.Songs.GetLyricsAsync(song.ID);
            await Console.Out.WriteLineAsync($"Synced lyrics:\r\n{lyricData.SyncedLyrics}\r\n\r\n");
            await Console.Out.WriteLineAsync($"Karaoke lyrics:\r\n{lyricData.GetEnhancedLyrics()}");
            //If you have VLC installed, you can play the audio directly by uncommenting the following line
            //Process.Start("vlc", mp3Url);
            await Console.Out.WriteLineAsync("------------------------------------");
        }

        static async Task GetArtistInformationAsync()
        {
            await Console.Out.WriteAsync($"Enter artist link: ");
            string link = await Console.In.ReadLineAsync() ?? "";
            var artist = await client.Artists.GetAsync(link);
            await Console.Out.WriteLineAsync($"Artist: {artist.Name}");
            await Console.Out.WriteLineAsync($"Real name: {artist.RealName}");
            await Console.Out.WriteLineAsync($"Birthday: {artist.Birthday}");
            await Console.Out.WriteLineAsync($"Nationality: {artist.Nationality}");
            await Console.Out.WriteLineAsync($"Biography:\r\n{artist.Biography}\r\n");
            var songs = await client.Artists.GetAllSongsAsync(artist.ID);
            await Console.Out.WriteLineAsync($"Songs: {songs.Count}");
            var mvs = await client.Artists.GetAllMVsAsync(artist.ID);
            await Console.Out.WriteLineAsync($"MVs: {mvs.Count}");
            await Console.Out.WriteLineAsync("------------------------------------");
        }

        static async Task GetAlbumInformationAsync()
        {
            await Console.Out.WriteAsync($"Enter album link: ");
            string link = await Console.In.ReadLineAsync() ?? "";
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
            string link = await Console.In.ReadLineAsync() ?? "";
            var mv = await client.Videos.GetAsync(link);
            await Console.Out.WriteLineAsync($"MV: {mv.Title}");
            await Console.Out.WriteLineAsync($"Duration: {mv.Duration}");
            await Console.Out.WriteLineAsync($"Artists: {mv.AllArtistsNames}");
            await Console.Out.WriteLineAsync($"Song: {mv.Song?.Title}");
            await Console.Out.WriteLineAsync($"Album: {mv.Album?.Title}");
            await Console.Out.WriteLineAsync($"HLS Stream: {mv.VideoStream.GetBestHLS()}");
            //If you have VLC installed, you can play the video directly by uncommenting the following line
            //System.Diagnostics.Process.Start(@"C:\Program Files\VideoLAN\VLC\vlc.exe", mv.VideoStream.GetBestHLS());
            await Console.Out.WriteLineAsync("------------------------------------");
        }
    }
}