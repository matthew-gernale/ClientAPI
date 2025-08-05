namespace ClientAPI.Server.Data.DTOs.AuthDTOs
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public CookieOptions CookieOptions { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
