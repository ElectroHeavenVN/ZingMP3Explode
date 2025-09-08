namespace ZingMP3Explode.Tests
{
    [TestClass]
    public class UnitTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestGetSong()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var song = await client.Songs.GetAsync("https://zingmp3.vn/bai-hat/Keo-Bong-Gon-Minh-Khon-Remix-H2K-HHD/Z60WDZB0.html", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("Z60WDZB0", song.ID);
                Assert.AreEqual("IW6ZZ0OU", song.Artists[0].ID);    //H2K
                Assert.AreEqual("IW76DFBF", song.Artists[1].ID);    //HHD
                Assert.AreEqual("6BZ6BUZE", song.Album?.ID);        //Kẹo Bông Gòn (Remix)
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
                Assert.AreEqual("IWZA6CCW", artist.ID);
                Assert.AreEqual("ZWZCU89E", artist.TopSongsPlaylistID);
                Assert.AreEqual("Norway", artist.Nationality);
                Assert.AreEqual("24/08/1997", artist.Birthday);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestGetAlbum()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var album = await client.Albums.GetAsync("https://zingmp3.vn/album/Fake-A-Smile-Remixes-EP-Alan-Walker-salem-ilese/676CZZ80.html", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("676CZZ80", album.ID);
                Assert.AreEqual("IWZA6CCW", album.Artists[0].ID);    //Alan Walker
                Assert.AreEqual("IW68F96C", album.Artists[1].ID);    //salem ilese
                Assert.AreEqual("19/03/2021", album.ReleaseDate);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestGetMV()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var mv = await client.Videos.GetAsync("https://zingmp3.vn/video-clip/Thang-Nam-Khong-Quen-EDM-Version-H2K-DJ-Eric-T-J/ZWAEWFCU.html", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("ZWAEWFCU", mv.ID);
                Assert.AreEqual("IW6ZZ0OU", mv.Artist?.ID);     //H2K
                Assert.AreEqual("IWZFEF8I", mv.Artists[1].ID);  //DJ Eric T-J
                Assert.AreEqual("ZWAEWFCU", mv.Song?.ID);
                Assert.AreEqual("Z6BA6ZB6", mv.Album?.ID);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestGetGenre()
        {
            Task.Run(async () =>
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync(TestContext.CancellationTokenSource.Token);
                var genre = await client.Genres.GetAsync("IWZ9Z08I", TestContext.CancellationTokenSource.Token);
                Assert.AreEqual("IWZ9Z08I", genre.ID);
                Assert.AreEqual("IWZ9Z08I", genre.Parent?.ID);
            }, TestContext.CancellationTokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}