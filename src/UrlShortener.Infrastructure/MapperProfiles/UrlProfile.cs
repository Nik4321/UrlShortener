using AutoMapper;
using UrlShortener.Data.Models.Dtos.Url;
using UrlShortener.Data.Models.Entities;

namespace UrlShortener.Infrastructure.MapperProfiles
{
    /// <summary>
    /// Profile for the auto mapper configuration for <see cref="ResponseUrl"/>
    /// </summary>
    public class UrlProfile : Profile
    {
        /// <summary>
        /// Constructs an instance of <see cref="UrlProfile"/>
        /// </summary>
        public UrlProfile()
        {
            CreateMap<Url, ResponseUrl>();
        }
    }
}
