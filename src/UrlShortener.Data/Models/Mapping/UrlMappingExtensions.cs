using UrlShortener.Data.Models.Dtos.Url;
using UrlShortener.Data.Models.Entities;

namespace UrlShortener.Data.Models.Mapping
{
    public static class UrlMappingExtensions
    {
        public static UrlResponse MapToDto(this Url entity)
        {
            return new UrlResponse
            {
                Id = entity.Id,
                ShortUrl = entity.ShortUrl,
                LongUrl = entity.LongUrl,
                ExpirationDate = entity.ExpirationDate, 
                CreatedOn = entity.CreatedOn,
            };
        }
    }
}