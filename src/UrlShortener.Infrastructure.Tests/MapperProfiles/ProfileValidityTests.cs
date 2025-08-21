using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Logging;
using UrlShortener.Infrastructure.MapperProfiles;
using Xunit;
using Xunit.Abstractions;

namespace UrlShortener.Infrastructure.Tests.MapperProfiles
{
    public class ProfileValidityTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public ProfileValidityTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AllProfilesShouldHaveValidConfiguration()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var sut = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(UrlProfile).Assembly);
            }, loggerFactory);

            var maps = sut.Internal().GetAllTypeMaps();

            foreach (var map in maps)
            {
                foreach (var prop in map.PropertyMaps)
                {
                    if (prop.SourceType != prop.DestinationType)
                    {
                        try
                        {
                            this.testOutputHelper.WriteLine(
                                $"Warning: {map.DestinationType} > {map.SourceType.Name} - {prop.SourceMember.Name} does not match {prop.DestinationMember.Name}");
                        }
                        catch { }
                    }
                }
            }

            sut.AssertConfigurationIsValid();
        }
    }
}
