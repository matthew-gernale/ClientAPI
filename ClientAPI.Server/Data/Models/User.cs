
namespace ClientAPI.Server.Data.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte[] Password { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public string RefreshToken { get; set; } = string.Empty;
        public UserRoles Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime RefreshTokenCreatedAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }

        public Client Client { get; set; }
    }
}
