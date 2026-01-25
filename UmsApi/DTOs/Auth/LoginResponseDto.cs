using UmsApi.DTOs.User;

namespace UmsApi.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public UserDto User { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}
