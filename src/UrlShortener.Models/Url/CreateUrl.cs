using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models.Url
{
    public class CreateUrl
    {
        [Required]
        [Url]
        public string LongUrl { get; set; }

        public long? ExpireDate { get; set; }
    }
}
