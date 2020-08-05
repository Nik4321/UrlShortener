using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Infrastructure.Models.Url
{
    [ExcludeFromCodeCoverage]
    public class CreateUrl
    {
        [Required]
        [Url]
        public string LongUrl { get; set; }

        public long? ExpireDate { get; set; }
    }
}
