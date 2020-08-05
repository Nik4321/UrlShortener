using System.Threading.Tasks;
using UrlShortener.Infrastructure.Models.Authorize;

namespace UrlShortener.Services
{
    public interface IUserService
    {
        Task<TokenResponse> AuthenticateUserAsync(CredentialsModel model);
    }
}
