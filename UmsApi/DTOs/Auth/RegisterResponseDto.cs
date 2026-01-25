using UmsApi.Models.Enums;

namespace UmsApi.DTOs.Auth;

public class RegisterResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
}
