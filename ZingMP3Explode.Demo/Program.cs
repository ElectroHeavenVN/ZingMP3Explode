#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
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
            Console.Clear();
            client = new ZingMP3Client();
            await client.InitializeAsync();
            int choice;
            do
            {
                await PrintMenuAsync();
                string? input = await Console.In.ReadLineAsync();
                if (!int.TryParse(input, out choice))
                {
                    await Console.Out.WriteLineAsync("Invalid input. Please enter a number.");
                    choice = -1;
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        await GetSongInformationAsync();
                        break;
                    case 2:
                        Console.Clear();
                        await GetArtistInformationAsync();
                        break;
                    case 3:
                        Console.Clear();
                        await GetAlbumInformationAsync();
                        break;
                    case 4:
                        Console.Clear();
                        await GetMVInformationAsync();
                        break;
                    case 5:
                        Console.Clear();
                        await GetZingChartAsync();
                        break;
                    case 0:
                        await Console.Out.WriteLineAsync("Exiting...");
                        break;
                    default:
                        await Console.Out.WriteLineAsync("Invalid choice. Please try again.");
                        break;
                }
                await Console.Out.WriteLineAsync();
                await Console.Out.WriteLineAsync("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            while (choice != 0);
        }

        static async Task PrintMenuAsync()
        {
            await Console.Out.WriteLineAsync("========== ZingMP3Explode Demo ==========");
            await Console.Out.WriteLineAsync("| 1. Get song information               |");
            await Console.Out.WriteLineAsync("| 2. Get artist information             |");
            await Console.Out.WriteLineAsync("| 3. Get album information              |");
            await Console.Out.WriteLineAsync("| 4. Get MV information                 |");
            await Console.Out.WriteLineAsync("| 5. Get Zing Chart                     |");
            await Console.Out.WriteLineAsync("| 0. Exit                               |");
            await Console.Out.WriteLineAsync("=========================================");
            await Console.Out.WriteAsync("Enter your choice: ");
        }

        static async Task GetZingChartAsync()
        {
            Console.WriteLine("             #zingchart");
            Console.WriteLine("------------------------------------");
            ZingChartData chart = await client.Chart.GetAsync();
            Console.WriteLine("Song recommendations: ");
            foreach (var song in chart.RealTimeChart.SongRecommendations)
                Console.WriteLine($"- {song.AllArtistsNames} - {song.Title}");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Top 3 songs this week:");
            for (int i = 0; i < 3; i++)
            {
                var song = chart.RealTimeChart.Items[i];
                Console.WriteLine($"{i + 1}. {song.AllArtistsNames} - {song.Title}");
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine("New releases:");
            foreach (var song in chart.NewlyReleasedSongs)
                Console.WriteLine($"- {song.AllArtistsNames} - {song.Title}");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Top 5 songs in the Vietnam weekly chart:");
            for (int i = 0; i < 5; i++)
            {
                var song = chart.WeeklyLeaderboard.Vietnam.Items[i];
                Console.WriteLine($"{i + 1}. {song.AllArtistsNames} - {song.Title}");
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Top 5 songs in the US-UK weekly chart:");
            for (int i = 0; i < 5; i++)
            {
                var song = chart.WeeklyLeaderboard.USUK.Items[i];
                Console.WriteLine($"{i + 1}. {song.AllArtistsNames} - {song.Title}");
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Top 5 songs in the K-Pop weekly chart:");
            for (int i = 0; i < 5; i++)
            {
                var song = chart.WeeklyLeaderboard.KPop.Items[i];
                Console.WriteLine($"{i + 1}. {song.AllArtistsNames} - {song.Title}");
            }

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
            await Console.Out.WriteLineAsync($"Karaoke lyrics:\r\n{lyricData.GetEnhancedLyrics(true)}");
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

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.