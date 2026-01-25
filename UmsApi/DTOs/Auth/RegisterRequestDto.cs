using System.ComponentModel.DataAnnotations;

namespace UmsApi.DTOs.Auth;

public class RegisterRequestDto
{
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public required string Password { get; set; }
}
