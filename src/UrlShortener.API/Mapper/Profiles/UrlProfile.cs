using AutoMapper;
using UrlShortener.Data.Models;
using UrlShortener.Infrastructure.Models.Url;

namespace UrlShortener.API.Mapper.Profiles
{
    public class UrlProfile : Profile
    {
        public UrlProfile()
        {
            CreateMap<Url, ResponseUrl>();
        }
    }
}
