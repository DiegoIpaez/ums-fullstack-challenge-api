namespace UmsApi.Models;

public class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!; // Siempre almacena el hash, nunca texto plano

    public string Role { get; set; } = "User";

    public bool Deleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Study> Studies { get; set; } = new List<Study>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<SessionLog> SessionLogs { get; set; } = new List<SessionLog>();
}
