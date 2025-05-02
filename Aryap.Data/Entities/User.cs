namespace Aryap.Data.Entities
{
    // Represents users in the database
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty; // Initialize with default value
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}