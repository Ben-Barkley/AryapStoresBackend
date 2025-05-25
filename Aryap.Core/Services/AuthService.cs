using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Aryap.Core.Services.Interfaces;
using Aryap.Data.Entities;
using Aryap.Shared.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Aryap.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(string username, string password, string email)
        {
            // Check if the username already exists
            var existingUser = await _userRepository.FindAsync(u => u.Username == username);
            if (existingUser.Any())
            {
                return "Username already exists.";
            }   

            // Hash the password
            var hashedPassword = ComputeSha256Hash(password);

            // Create a new user
            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Email = email
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return "User registered successfully.";
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            // Find the user by username
            var user = await _userRepository.FindAsync(u => u.Username == username);
            if (!user.Any() || user.First().PasswordHash != ComputeSha256Hash(password))
            {
                return "Invalid username or password.";
            }

            // Generate a JWT token
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.First().Username),
                new Claim(ClaimTypes.NameIdentifier, user.First().UserId.ToString())
            });

            var token = GenerateJwtToken(identity);
            return token;
        }

        public string GenerateJwtToken(ClaimsIdentity identity)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException("Jwt:Key is missing in appsettings.json");
            }

            var key = Encoding.ASCII.GetBytes(jwtKey);
            if (key.Length != 32)
            {
                throw new ArgumentException("JWT key must be 256 bits (32 bytes) long.");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using var sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}