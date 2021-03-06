﻿using System.Threading.Tasks;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Repositories.BaseRepositories;

namespace UrlShortener.Repositories
{
    public interface IUrlRepository : IBaseRepository<Url, int>
    {
        Task<Url> GetByShortUrl(string shortUrl);
    }
}
