namespace UmsApi.Models;

public class Study
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string Institution { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = null!;
}
