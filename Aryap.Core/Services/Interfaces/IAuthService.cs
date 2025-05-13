using System.Security.Claims;

namespace Aryap.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(string username, string password, string email);
        Task<string> LoginAsync(string username, string password);
        string GenerateJwtToken(ClaimsIdentity identity);
    }
}



