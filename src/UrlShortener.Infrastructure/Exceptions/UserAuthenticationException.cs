using System;
using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Infrastructure.Exceptions
{
    /// <summary>
    /// A custom <see cref="Exception"/> that is thrown when user has not authenticated succesfully.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UserAuthenticationException : ApplicationException
    {
        /// <summary>
        /// Creates an instace of <see cref="UserAuthenticationException"/>
        /// </summary>
        /// <param name="message">Message to be passed as <see cref="Exception.Message"/></param>
        public UserAuthenticationException(string message) : base(message)
        {
        }
    }
}
