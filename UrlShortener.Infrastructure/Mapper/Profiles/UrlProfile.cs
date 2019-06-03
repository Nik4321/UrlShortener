using System.Security.Policy;
using AutoMapper;
using UrlShortener.Models.Url;

namespace UrlShortener.Infrastructure.Mapper.Profiles
{
    public class UrlProfile : Profile
    {
        public UrlProfile()
        {
            CreateMap<Url, ResponseUrl>();
        }
    }
}
