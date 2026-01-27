using System.Threading;
using System.Threading.Tasks;
using ZingMP3Explode.Entities;
using ZingMP3Explode.Exceptions;
using ZingMP3Explode.Utilities;

namespace ZingMP3Explode.Clients
{
    /// <summary>
    /// <para xml:lang="en">Provides methods to access information about #zingchart leaderboards.</para>
    /// <para xml:lang="vi">Cung cấp các phương thức để truy cập thông tin về bảng xếp hạng #zingchart.</para>
    /// </summary>
    public class ZingChartClient
    {
        readonly ZingMP3Client zClient;

        internal ZingChartClient(ZingMP3Client client)
        {
            zClient = client;
        }

        /// <summary>
        /// <para xml:lang="en">Gets the #zingchart leaderboard information.</para>
        /// <para xml:lang="vi">Lấy thông tin bảng xếp hạng #zingchart.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">The #zingchart leaderboard information.</para>
        /// <para xml:lang="vi">Thông tin bảng xếp hạng #zingchart.</para>
        /// </returns>
        public async Task<ZingChartData> GetAsync(CancellationToken cancellationToken = default)
        {
            return await zClient.APIClient.GetZingChartAsync(cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets the weekly leaderboard information.</para>
        /// <para xml:lang="vi">Lấy thông tin bảng xếp hạng hàng tuần.</para>
        /// </summary>
        /// <param name="id">
        /// <para xml:lang="en">The leaderboard ID.</para>
        /// <para xml:lang="vi">ID bảng xếp hạng.</para>
        /// </param>
        /// <param name="week">
        /// <para xml:lang="en">The week number. Default is current week.</para>
        /// <para xml:lang="vi">Số tuần. Mặc định tuần hiện tại.</para>
        /// </param>
        /// <param name="year">
        /// <para xml:lang="en">The year number. Default is current year.</para>
        /// <para xml:lang="vi">Số năm. Mặc định năm hiện tại.</para>
        /// </param>
        /// <returns>
        /// <para xml:lang="en">The weekly leaderboard information.</para>
        /// <para xml:lang="vi">Thông tin bảng xếp hạng hàng tuần.</para>
        /// </returns>
        public async Task<WeeklyLeaderboard> GetWeeklyLeaderboardRegionAsync(string id, int week = 0, int year = 0, CancellationToken cancellationToken = default)
        {
            if (!Regexes.WeeklyLeaderboardID.IsMatch(id))
                throw new ZingMP3ExplodeException("Invalid leaderboard ID");
            if (week != 0 && (week < 0 || week > 53))
                throw new ZingMP3ExplodeException("Week must be between 0 and 53");
            if (year != 0 && year < 2007)
                throw new ZingMP3ExplodeException("Year must be greater than or equal to 2007");
            return await zClient.APIClient.GetWeeklyLeaderboardRegionAsync(id, week, year, cancellationToken);
        }

        /// <summary>
        /// <para xml:lang="en">Gets newly released songs.</para>
        /// <para xml:lang="vi">Lấy các bài hát mới phát hành.</para>
        /// </summary>
        /// <returns>
        /// <para xml:lang="en">A page section containing newly released songs.</para>
        /// <para xml:lang="vi">Một phân vùng trang chứa các bài hát mới phát hành.</para>
        /// </returns>
        public async Task<PageSection<Song>> GetNewlyReleasedSongsAsync(CancellationToken cancellationToken = default)
        {
            return await zClient.APIClient.GetNewlyReleasedSongsAsync(cancellationToken);
        }
    }
}
