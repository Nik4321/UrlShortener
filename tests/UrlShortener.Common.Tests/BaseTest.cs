using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;

namespace UrlShortener.Services.Tests.Base
{
    public abstract class BaseTest
    {
        protected UrlShortenerDbContext db;

        public void BaseSetup()
        {
            this.db = InstantiateDbContext();
        }

        private static UrlShortenerDbContext InstantiateDbContext()
        {
            var dbOptions = new DbContextOptionsBuilder<UrlShortenerDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            var db = new UrlShortenerDbContext(dbOptions);
            return db;
        }
    }
}
