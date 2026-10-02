namespace ZingMP3Explode.Tests
{
    [TestClass]
    public class UnitTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestGetSong_OldID()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var song = await client.Songs.GetAsync("https://zingmp3.vn/bai-hat/Idol-YOASOBI/Z6ACDOWD.html", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("18MP1PSDbHpn", song.ID);
                Assert.AreEqual("Idol-YOASOBI", song.Alias);
                Assert.AreEqual(null, song.Artist);
                Assert.AreEqual("JmnpWhAOjSmO", song.Artists[0].ID);    //YOASOBI
                Assert.AreEqual("IkXFneD83VIA", song.Album?.ID);        //Idol (Single)
                TestContext.WriteLine("Title: " + song.Title);
                TestContext.WriteLine("Artists: " + song.AllArtistsNames);
                TestContext.WriteLine("Album: " + song.Album?.Title);
                TestContext.WriteLine("Distributor: " + song.Distributor);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestGetSong()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var song = await client.Songs.GetAsync("https://zingmp3.vn/bai-hat/Keo-Bong-Gon-Minh-Khon-Remix-H2K-HHD/xlGk5v9NQ08.html", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("xlGk5v9NQ08", song.ID);
                Assert.AreEqual("Keo-Bong-Gon-Minh-Khon-Remix-H2K-HHD", song.Alias);
                Assert.AreEqual(null, song.Artist);
                Assert.AreEqual("tj4ULnpqne1y", song.Artists[0].ID);    //H2K
                Assert.AreEqual("r4q2d9hhq4lX", song.Artists[1].ID);    //HHD
                Assert.AreEqual("xAmhQLfHVjfE", song.Album?.ID);        //Kẹo Bông Gòn (Remix)
                TestContext.WriteLine("Title: " + song.Title);
                TestContext.WriteLine("Artists: " + song.AllArtistsNames);
                TestContext.WriteLine("Album: " + song.Album?.Title);
                TestContext.WriteLine("Distributor: " + song.Distributor);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestGetArtist()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var artist = await client.Artists.GetAsync("https://zingmp3.vn/Alan-Walker", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("Alan-Walker", artist.Alias);
                Assert.AreEqual("V8JgPEDvCwAH", artist.ID);
                Assert.AreEqual("nl8hImvW54mU", artist.TopSongsPlaylistID);
                Assert.AreEqual("Norway", artist.Nationality);
                Assert.AreEqual("24/08/1997", artist.Birthday);
                TestContext.WriteLine("Name: " + artist.Name);
                TestContext.WriteLine("Biography: " + artist.Biography);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestGetAlbum()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var album = await client.Albums.GetAsync("https://zingmp3.vn/album/Fake-A-Smile-Remixes-EP-Alan-Walker-salem-ilese/u7U3Q25TCcg0.html", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("Fake-A-Smile-Remixes-EP-Alan-Walker-salem-ilese", album.Alias);
                Assert.AreEqual("u7U3Q25TCcg0", album.ID);
                Assert.AreEqual("V8JgPEDvCwAH", album.Artists[0].ID);    //Alan Walker
                Assert.AreEqual("ur8KcjO8fWiL", album.Artists[1].ID);    //salem ilese
                Assert.AreEqual("19/03/2021", album.ReleaseDate);
                TestContext.WriteLine("Title: " + album.Title);
                TestContext.WriteLine("Description: " + album.Description);
                TestContext.WriteLine("Song count: " + album.Songs.Total);
                TestContext.WriteLine("Distributor: " + album.Distributor);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestGetMV()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var mv = await client.Videos.GetAsync("https://zingmp3.vn/video-clip/Thang-Nam-Khong-Quen-EDM-Version-H2K-DJ-Eric-T-J/fr47lwzJp3x.html", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("Thang-Nam-Khong-Quen-EDM-Version-H2K-DJ-Eric-T-J", mv.Alias);
                Assert.AreEqual("fr47lwzJp3x", mv.ID);
                Assert.AreEqual("tj4ULnpqne1y", mv.Artist?.ID);     //H2K
                Assert.AreEqual("ySsTyYuTENnN", mv.Artists[1].ID);  //DJ Eric T-J
                Assert.AreEqual("fr47lwzJp3x", mv.Song?.ID);
                Assert.AreEqual("oflzEugfOmZC", mv.Album?.ID);
                TestContext.WriteLine("Title: " + mv.Title);
                TestContext.WriteLine("Artists: " + mv.AllArtistsNames);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestGetGenre()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var genre = await client.Genres.GetAsync("el0RHYY9aI0v", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("viet-nam", genre.Alias);
                Assert.AreEqual("el0RHYY9aI0v", genre.ID);
                Assert.AreEqual("viet-nam", genre.Parent?.Alias);
                Assert.AreEqual("el0RHYY9aI0v", genre.Parent?.ID);
                TestContext.WriteLine("Name: " + genre.Name);
                TestContext.WriteLine("Title: " + genre.Title);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestGetZingChart()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var chart = await client.Chart.GetAsync(TestContext.CancellationTokenSource.Token);
                // chart value change frequently, so we don't assert specific values here
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}