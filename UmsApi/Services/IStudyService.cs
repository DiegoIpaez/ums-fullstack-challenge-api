using UmsApi.DTOs.Study;

namespace UmsApi.Services;

public interface IStudyService
{
    Task<List<StudyResponseDto>> GetAllAsync();
    Task<StudyResponseDto?> GetByIdAsync(long id);
    Task<StudyResponseDto> CreateAsync(StudyCreateDto dto);
    Task UpdateAsync(long id, StudyUpdateDto dto);
    Task DeleteAsync(long id);
}
