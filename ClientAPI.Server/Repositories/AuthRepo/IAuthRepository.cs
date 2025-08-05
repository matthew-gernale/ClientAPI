namespace ClientAPI.Server.Repositories.AuthRepo
{
    public interface IAuthRepository
    {
        Task<LoginResponse> Login(LoginDTO request);
        LoginResponse Logout();
        Task<LoginResponse?> ReRefreshToken(string? refToken);
    }
}
