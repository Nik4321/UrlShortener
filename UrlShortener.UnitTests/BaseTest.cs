using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using UrlShortener.Data;

namespace UrlShortener.UnitTests
{
    public abstract class BaseTest
    {
        protected UrlShortenerDbContext db;

        [SetUp]
        public virtual void SetUp()
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
