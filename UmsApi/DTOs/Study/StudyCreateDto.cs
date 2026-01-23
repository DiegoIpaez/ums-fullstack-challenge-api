namespace UmsApi.DTOs.Study;

public class StudyCreateDto
{
    public string Title { get; set; } = null!;
    public string Institution { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long UserId { get; set; }
}
