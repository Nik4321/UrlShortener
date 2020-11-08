using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Infrastructure.Constants;

namespace UrlShortener.Data.Extensions
{
    public static class DataSeedExtensions
    {
        private static readonly string[] roles = new[]
        {
            RoleNamesConstants.Admin
        };

        public static async Task SeedDatabase(this UrlShortenerDbContext db, RoleManager<UserRole> roleManager)
        {
            await SeedRoles(roleManager);
        }

        private static async Task SeedRoles(RoleManager<UserRole> roleManager)
        {
            foreach (var role in roles)
            {
                if (await roleManager.RoleExistsAsync(role)) continue;
                var newRole = new UserRole { Name = role };
                await roleManager.CreateAsync(newRole);
            }
        }
    }
}
