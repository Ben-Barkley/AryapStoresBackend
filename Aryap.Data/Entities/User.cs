using Aryap.Shared.Abstractions;

namespace Aryap.Data.Entities
{
    // Represents users in the database
    public class User : BaseEntity  
    {
       
        public string Username { get; set; } = string.Empty; // Initialize with default value
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
      
    }
}