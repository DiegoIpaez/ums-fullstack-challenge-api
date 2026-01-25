using System.ComponentModel.DataAnnotations;

namespace UmsApi.DTOs.Study;

public class StudyCreateDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Institution { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public long UserId { get; set; }
}
