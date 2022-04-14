
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.AuthModels;
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : Controller
    {
        private readonly IAuthService _accountService;
        private readonly ITokenManager _tokenManager;

        public AuthController(IAuthService accountService,
                ITokenManager tokenManager)
        {
            _accountService = accountService;
            _tokenManager = tokenManager;
        }

        [HttpGet("account")]
        public IActionResult Get([FromBody] SignUp request)
            => Content($"Hello {User.Identity.Name}");

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public IActionResult SignUp([FromBody] SignUp request)
        {
            _accountService.SignUp(request.Username, request.Password);

            return NoContent();
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public IActionResult SignIn([FromBody] SignIn request)
            => Ok(_accountService.SignIn(request.Username, request.Password));

        [HttpPost("tokens/{token}/refresh")]
        [AllowAnonymous]
        public IActionResult RefreshAccessToken(string token)
            => Ok(_accountService.RefreshAccessToken(token));

        [HttpPost("tokens/{token}/revoke")]
        public IActionResult RevokeRefreshToken(string token)
        {
            _accountService.RevokeRefreshToken(token);

            return NoContent();
        }

        [HttpPost("tokens/cancel")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _tokenManager.DeactivateCurrentAsync();

            return NoContent();
        }
    }
}