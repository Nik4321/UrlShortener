using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;

namespace UrlShortener.UnitTests
{
    public abstract class BaseTest
    {
        protected UrlShortenerDbContext db;

        public void BaseSetUp()
        {
            this.db = SetUpDbContext();
        }

        private static UrlShortenerDbContext SetUpDbContext()
        {
            var dbOptions = new DbContextOptionsBuilder<UrlShortenerDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            var db = new UrlShortenerDbContext(dbOptions);
            return db;
        }
    }
}
