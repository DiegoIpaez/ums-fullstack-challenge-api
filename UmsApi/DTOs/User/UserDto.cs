using UmsApi.Models.Enums;

namespace UmsApi.DTOs.User;

public class UserDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }

    public List<StudyDto> Studies { get; set; } = new();
    public List<AddressDto> Addresses { get; set; } = new();
}
