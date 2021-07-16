using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Infrastructure.Constants
{
    /// <summary>
    /// Exception messages constant values
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ExceptionMessagesConstants
    {
        /// <summary>
        /// Invalid url exception message value
        /// </summary>
        public const string InvalidUrlExceptionMessage = "Invalid long url";
    }
}
