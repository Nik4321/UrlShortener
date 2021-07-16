#pragma warning disable 1591
using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Infrastructure.Settings
{
    [ExcludeFromCodeCoverage]
    public class JwtSettings
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
    }
}
