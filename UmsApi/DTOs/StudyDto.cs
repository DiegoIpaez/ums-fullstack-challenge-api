namespace UmsApi.DTOs;

public class StudyDto
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
