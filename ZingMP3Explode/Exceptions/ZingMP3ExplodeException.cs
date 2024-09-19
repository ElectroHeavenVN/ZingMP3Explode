using System;
using System.Diagnostics;

namespace ZingMP3Explode.Exceptions
{
    /// <summary>
    /// Exception thrown within <see cref="ZingMP3Explode"/>.
    /// </summary>
    [DebuggerDisplay("{ErrorCode}: {Message}")]
    public class ZingMP3ExplodeException : Exception
    {
        /// <summary>
        /// Error code of the exception.
        /// </summary>
        public int ErrorCode { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ZingMP3ExplodeException"/> class.
        /// </summary>
        internal ZingMP3ExplodeException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ZingMP3ExplodeException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"><inheritdoc/></param>
        internal ZingMP3ExplodeException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZingMP3ExplodeException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        internal ZingMP3ExplodeException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZingMP3ExplodeException"/> class with a specified error message and error code.
        /// </summary>
        /// <param name="errorCode">Error code of the exception.</param>
        /// <param name="message">The message that describes the error.</param>
        internal ZingMP3ExplodeException(int errorCode, string message) : base(message) => ErrorCode = errorCode;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{ErrorCode}: {Message}";
        }
    }
}
