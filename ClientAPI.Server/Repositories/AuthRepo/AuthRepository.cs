

namespace ClientAPI.Server.Repositories.AuthRepo
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _config;

        public AuthRepository(DataContext context,
            IHttpContextAccessor contextAccessor,
            IConfiguration config)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _config = config;
        }


        public async Task<LoginResponse> Login(LoginDTO request)
        {
            User? user = await _context.Users
                 .FirstOrDefaultAsync(user => user.Email == request.Email && user.IsActive == true);

            LoginResponse response = new LoginResponse();

            if (
                user != null
                && AuthUtil.VerifyPasswordHash(request.Password, user.Password, user.PasswordSalt)
            )
            {
                if (user.Role == UserRoles.ADMIN ||
                    user.Role == UserRoles.CLIENT)
                {
                    string token = AuthUtil.CreateToken(user, _config);
                    RefreshToken refreshToken = AuthUtil.GenerateRefreshToken(user, _config);

                    CookieOptions cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = refreshToken.ExpiresAt
                    };

                    user.RefreshToken = refreshToken.Token;
                    user.RefreshTokenCreatedAt = refreshToken.CreatedAt;
                    user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

                    await _context.SaveChangesAsync();

                    response.CookieOptions = cookieOptions;
                    response.AccessToken = token;
                    response.RefreshToken = refreshToken.Token;

                    return response;
                }
            }
            return response;
        }

        public LoginResponse Logout()
        {
            return AuthUtil.DeleteRefreshToken();
        }

        public async Task<LoginResponse?> ReRefreshToken(string? refToken)
        {
            if (refToken == null) return null;

            var claims = AuthUtil.ValidateRefreshToken(refToken, _config);
            if (claims == null) return null;

            Guid.TryParse(UserUtils.GetUserId(_contextAccessor), out Guid userId);

            User? db_user = await _context.Users
                .Where(user => user.Id == userId)
                .FirstOrDefaultAsync();

            if (db_user == null) return null;

            if (!db_user.RefreshToken.Equals(refToken)) return null;

            if (db_user.RefreshTokenExpiresAt < DateTime.Now)
            {
                var res = AuthUtil.DeleteRefreshToken();
                return res;
            }

            LoginResponse response = new LoginResponse();

            string token = AuthUtil.CreateToken(db_user, _config);
            RefreshToken refreshToken = AuthUtil.GenerateRefreshToken(db_user, _config);

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiresAt
            };

            db_user.RefreshToken = refreshToken.Token;
            db_user.RefreshTokenCreatedAt = refreshToken.CreatedAt;
            db_user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

            await _context.SaveChangesAsync();

            response.CookieOptions = cookieOptions;
            response.AccessToken = token;
            response.RefreshToken = refreshToken.Token;

            return response;
        }
    }
}
