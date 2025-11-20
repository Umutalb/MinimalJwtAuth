using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.API.Entities;
using SimpleAuth.API.Models;
using SimpleAuth.API.Services;

namespace SimpleAuth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = _userService.Register(request.Email, request.Password);
                return Ok(new { message = "User registered successfully", user.Email });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.Login(request.Email, request.Password);

            if (user is null)
                return Unauthorized(new { error = "Invalid email or password" });

            var token = _tokenService.CreateToken(user);
            return Ok(token);
        }
    }
}
