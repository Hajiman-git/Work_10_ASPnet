using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using work_10.Dtos.Auth;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(
            [FromBody] RegisterDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var result = await _authService.RegisterAsync(dto, ct);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        [EnableRateLimiting("LoginLimiter")]
        public async Task<ActionResult<AuthResponseDto>> Login(
            [FromBody] LoginDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var result = await _authService.LoginAsync(dto, ct);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}