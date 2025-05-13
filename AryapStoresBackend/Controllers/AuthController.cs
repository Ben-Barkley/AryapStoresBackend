using Aryap.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aryap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request.Username, request.Password, request.Email);
            return result.Contains("successfully") ? Ok(new { Message = result }) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Username, request.Password);
            return token.Contains("Invalid") ? Unauthorized(token) : Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Simply return a success message. The client will handle token removal.
            return Ok(new { Message = "Logout successful" });
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
