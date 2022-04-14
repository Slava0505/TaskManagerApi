using TaskManagerApi.AuthModels;

namespace TaskManagerApi.Services
{
    public interface IAuthService
    {
        void SignUp(string username, string password);
        JsonWebToken SignIn(string username, string password);
        JsonWebToken RefreshAccessToken(string token);
        void RevokeRefreshToken(string token);
    }
}