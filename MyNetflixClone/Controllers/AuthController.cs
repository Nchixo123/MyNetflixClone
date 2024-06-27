using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ServiceInterfaces;

namespace MyNetflixClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthRequest authRequest)
        {
            var user = await _userService.ValidateUserAsync(authRequest.Username, authRequest.Password);

            if (user != null)
            {
                var token = _tokenService.GenerateToken(user.Username);
                return Ok(token);
            }

            return Unauthorized(new { Message = "Invalid username or password" });
        }
    }
}
