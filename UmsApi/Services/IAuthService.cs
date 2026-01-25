using UmsApi.DTOs.Auth;
using UmsApi.DTOs.User;

namespace UmsApi.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
    Task LogoutAsync(long userId);
}
