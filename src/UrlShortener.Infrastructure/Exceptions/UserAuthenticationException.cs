using System;

namespace UrlShortener.Infrastructure.Exceptions
{
    public class UserAuthenticationException : ApplicationException
    {
        public UserAuthenticationException(string message) : base(message)
        {
        }
    }
}
