using work_10.Dtos.Auth;

namespace work_10.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken ct = default);
        Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken ct = default);
    }
}