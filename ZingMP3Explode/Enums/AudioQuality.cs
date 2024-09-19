namespace ZingMP3Explode.Enums
{
    /// <summary>
    /// Quality of the audio file/stream.
    /// </summary>
    public enum AudioQuality
    {
        /// <summary>
        /// Select the highest quality available.
        /// </summary>
        Best,

        /// <summary>
        /// Lossless quality (FLAC), only available for some songs if the logged in account has a Plus/Premium account.
        /// </summary>
        Lossless,

        /// <summary>
        /// High quality (320kbps), only available for some songs if the user is logged in.
        /// </summary>
        High,
        /// <summary>
        /// Normal quality (128kbps).
        /// </summary>
        Normal
    }
}
