using System;

namespace ZingMP3Explode.Exceptions
{
    /// <summary>
    /// <para xml:lang="en">Exception thrown by the library.</para>
    /// <para xml:lang="vi">Ngoại lệ được ném ra bởi thư viện.</para>
    /// </summary>
    public class ZingMP3ExplodeException : Exception
    {
        internal ZingMP3ExplodeException() : base() { }
        internal ZingMP3ExplodeException(string message) : base(message) { }
        internal ZingMP3ExplodeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
