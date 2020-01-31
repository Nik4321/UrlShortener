using System;

namespace UrlShortener.Infrastructure.Exceptions
{
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException(string message) : base(message)
        {
        }

        public InvalidUrlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}