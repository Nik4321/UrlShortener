using System;
using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Data.Models
{
    [ExcludeFromCodeCoverage]
    public class Url : BaseEntity<int>
    {
        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
