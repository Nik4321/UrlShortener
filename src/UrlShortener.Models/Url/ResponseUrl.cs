using System;

namespace UrlShortener.Models.Url
{
    public class ResponseUrl
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }
    }
}
