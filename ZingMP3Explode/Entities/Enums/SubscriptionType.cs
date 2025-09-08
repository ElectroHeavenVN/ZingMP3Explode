namespace ZingMP3Explode.Entities
{
    /// <summary>
    /// <para xml:lang="en">ZingMP3's subscription type.</para>
    /// <para xml:lang="vi">Loại gói đăng ký của ZingMP3.</para>
    /// </summary>
    public enum SubscriptionType
    {
        /// <summary>
        /// <para xml:lang="en">Unknown subscription type.</para>
        /// <para xml:lang="vi">Loại đăng ký không xác định.</para>
        /// </summary>
        Unknown,

        /// <summary>
        /// <para xml:lang="en">Free, no subscription.</para>
        /// <para xml:lang="vi">Miễn phí, không đăng ký gói nào.</para>
        /// </summary>
        Free,

        /// <summary>
        /// <para xml:lang="en">Plus subscription.</para>
        /// <para xml:lang="vi">Gói đăng ký Plus.</para>
        /// </summary>
        Plus,

        /// <summary>
        /// <para xml:lang="en">Premium subscription.</para>
        /// <para xml:lang="vi">Gói đăng ký Premium.</para>
        /// </summary>
        Premium
    }
}
