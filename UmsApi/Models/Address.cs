namespace UmsApi.Models;

public class Address
{
    public long Id { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public long UserId { get; set; }
    public User User { get; set; } = null!;
}
