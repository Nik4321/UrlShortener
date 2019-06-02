using System;

namespace UrlShortener.Data.Models
{
    public class Url : BaseEntity<int>
    {
        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string ExpireLinkUrl { get; set; }
    }
}
