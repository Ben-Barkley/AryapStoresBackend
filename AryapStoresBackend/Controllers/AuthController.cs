using Aryap.Core.Services.Interfaces;

namespace Aryap.API.Controllers
{
    public class AuthController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
    }
}
