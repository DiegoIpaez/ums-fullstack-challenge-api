using System.ComponentModel.DataAnnotations;

namespace UmsApi.DTOs.Address;

public class AddressCreateDto
{
    [Required]
    [StringLength(100)]
    public string Street { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string City { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string State { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Country { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string PostalCode { get; set; } = null!;
}
