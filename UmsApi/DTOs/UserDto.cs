namespace UmsApi.DTOs;

public class UserDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;

    public List<StudyDto> Studies { get; set; } = new();
    public List<AddressDto> Addresses { get; set; } = new();
}
