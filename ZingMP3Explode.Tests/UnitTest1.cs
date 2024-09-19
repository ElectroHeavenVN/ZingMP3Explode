using System.Diagnostics;
using System.Net;
using ZingMP3Explode;

namespace ZingMP3Explode.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Test1().Wait();
        }

        public async Task Test1()
        {
            try
            {
                ZingMP3Client client = new ZingMP3Client();
                await client.InitializeAsync();
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }
    }
}