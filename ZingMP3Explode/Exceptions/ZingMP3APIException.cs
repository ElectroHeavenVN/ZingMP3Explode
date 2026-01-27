using System.Diagnostics;

namespace ZingMP3Explode.Exceptions
{
    /// <summary>
    /// <para xml:lang="en">The exception that is thrown when an error occurs while accessing the API.</para>
    /// <para xml:lang="vi">Ngoại lệ được ném ra khi có lỗi xảy ra trong quá trình truy cập API.</para>
    /// </summary>
    [DebuggerDisplay($"API exception: {{{nameof(ErrorCode)}}}: {{{nameof(Message)}}}")]
    public class ZingMP3APIException : ZingMP3ExplodeException
    {
        /// <summary>
        /// <para xml:lang="en">The error code returned by the API.</para>
        /// <para xml:lang="vi">Mã lỗi được trả về từ API.</para>
        /// </summary>
        public int ErrorCode { get; }

        internal ZingMP3APIException() : base() { }

        internal ZingMP3APIException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return base.ToString().Replace(Message, $"{ErrorCode}: {Message}");
        }
    }
}
