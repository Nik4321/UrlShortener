﻿using AutoMapper;
using UrlShortener.Data.Models;
using UrlShortener.Infrastructure.Models.Url;

namespace UrlShortener.API.Mapper.Profiles
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
