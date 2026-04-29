using BookingForHumanService.Application.DTOs.AuthDtos;
using BookingForHumanService.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingForHumanService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private ILogger<AuthController> _logger;
        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            _logger.LogInformation("Getting Register");
            await _authService.RegisterAsync(dto);

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginDto dto)
        {
            _logger.LogInformation("Getting Logging");

            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("logout")]
      
        public async Task<IActionResult> Logout()
        {
          
            return Ok(new { message = "Logout successful" });
        }

    }
}
