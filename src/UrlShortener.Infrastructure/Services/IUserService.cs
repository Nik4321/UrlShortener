#pragma warning disable CS1591
using System.Threading.Tasks;
using UrlShortener.Data.Models.Dtos.Authorize;

namespace UrlShortener.Infrastructure.Services
{
    public interface IUserService
    {
        Task<TokenResponse> AuthenticateUserAsync(CredentialsModel model);
    }
}
