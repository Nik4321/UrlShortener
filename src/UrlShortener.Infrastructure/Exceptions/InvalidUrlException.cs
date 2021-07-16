using System;
using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Infrastructure.Exceptions
{
    /// <summary>
    /// A custom <see cref="Exception"/> that is thrown when url is invalid
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class InvalidUrlException : Exception
    {
        /// <summary>
        /// Creates an instace of <see cref="InvalidUrlException"/>
        /// </summary>
        /// <param name="message">Message to be passed as <see cref="Exception.Message"/></param>
        public InvalidUrlException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates an instace of <see cref="InvalidUrlException"/>
        /// </summary>
        /// <param name="message">Message to be passed as <see cref="Exception.Message"/></param>
        /// <param name="innerException">The inner exception to be passed as <see cref="Exception.InnerException"/></param>
        public InvalidUrlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}