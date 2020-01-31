using System.Threading.Tasks;
using UrlShortener.Models.Authorize;

namespace UrlShortener.Services
{
    public interface IUserService
    {
        Task<TokenResponse> AuthenticateUserAsync(CredentialsModel model);
    }
}
