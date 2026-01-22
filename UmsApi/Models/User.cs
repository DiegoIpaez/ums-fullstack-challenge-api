namespace UmsApi.Models;

public class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = "User"; 

    public bool Deleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Study> Studies { get; set; } = new List<Study>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}
