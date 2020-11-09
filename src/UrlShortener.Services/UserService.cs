using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data;
using UrlShortener.Data.Models.Dtos.Authorize;
using UrlShortener.Data.Models.Entities;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.Infrastructure.Settings;

namespace UrlShortener.Services
{
    public class UserService : IUserService
    {
        private readonly UrlShortenerDbContext db;
        private readonly UserManager<User> userManager;
        private readonly JwtSettings jwtSettings;

        public UserService(
            UrlShortenerDbContext db,
            UserManager<User> userManager,
            IOptions<JwtSettings> jwtSettings)
        {
            this.db = db;
            this.userManager = userManager;
            this.jwtSettings = jwtSettings.Value;
        }

        public async Task<TokenResponse> AuthenticateUserAsync(CredentialsModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);

            var isPasswordCorrect = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !isPasswordCorrect)
            {
                throw new UserAuthenticationException("Unauthorized");
            }

            var token = await this.GenerateUserToken(user);

            var result = new TokenResponse
            {
                UserEmail = user.Email,
                Type = "bearer",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };

            return result;
        }

        private async Task<JwtSecurityToken> GenerateUserToken(User user)
        {
            var roles = await this.userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                this.jwtSettings.Authority,
                this.jwtSettings.Audience,
                claimsIdentity.Claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return token;
        }
    }
}