using AuthenService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenService.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly RefreshTokenService _refreshService;

        public AuthController(
            JwtService jwtService,
            RefreshTokenService refreshService)
        {
            _jwtService = jwtService;
            _refreshService = refreshService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "1234")
            {

                var user = new User
                {
                    Id = 1,
                    Username = "admin",
                    Role = "Admin"
                };

                var accessToken = _jwtService.GenerateToken(user);

                var refreshToken = _refreshService.GenerateToken();

                return Ok(new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshRequest request)
        {
            // TODO: validate refresh token from DB

            var user = new User
            {
                Id = 1,
                Username = "admin",
                Role = "Admin"
            };

            var newAccessToken = _jwtService.GenerateToken(user);

            var newRefreshToken = _refreshService.GenerateToken();

            return Ok(new LoginResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // revoke refresh token
            return Ok();
        }
    }
}
