using System.Threading.Tasks;
using UrlShortener.Data.Models.Dtos.Authorize;

namespace UrlShortener.Services
{
    public interface IUserService
    {
        Task<TokenResponse> AuthenticateUserAsync(CredentialsModel model);
    }
}
