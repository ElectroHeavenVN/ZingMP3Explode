namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="vi">Chất lượng âm thanh.</para>
    /// <para xml:lang="en">Quality of the audio file/stream.</para>
    /// </summary>
    public enum AudioQuality
    {
        /// <summary>
        /// <para xml:lang="en">The best available quality.</para>
        /// <para xml:lang="vi">Chất lượng tốt nhất có thể.</para>
        /// </summary>
        Best,

        /// <summary>
        /// <para xml:lang="en">Lossless quality (FLAC), only available for some songs if the logged in account has a Plus/Premium subscription.</para>
        /// <para xml:lang="vi">Chất lượng Lossless (FLAC), chỉ có sẵn cho một số bài hát nếu tài khoản đã đăng nhập có đăng ký Plus/Premium.</para>
        /// </summary>
        Lossless,

        /// <summary>
        /// <para xml:lang="en">High quality (320kbps), only available for some songs if the login credentials are provided.</para>
        /// <para xml:lang="vi">Chất lượng cao (320kbps), chỉ có sẵn cho một số bài hát nếu thông tin đăng nhập được cung cấp.</para>
        /// </summary>
        High,

        /// <summary>
        /// <para xml:lang="en">Normal quality (128kbps).</para>
        /// <para xml:lang="vi">Chất lượng bình thường (128kbps).</para>
        /// </summary>
        Normal
    }
}
