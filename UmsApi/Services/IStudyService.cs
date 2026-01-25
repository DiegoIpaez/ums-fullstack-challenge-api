using UmsApi.DTOs;
using UmsApi.DTOs.Study;

namespace UmsApi.Services;

public interface IStudyService
{
    Task<PaginatedResponseDto<StudyResponseDto>> GetAllAsync(
        int page = 1,
        int limit = 10,
        bool showAll = false,
        string? search = null
    );
    Task<StudyResponseDto?> GetByIdAsync(long id);
    Task<StudyResponseDto> CreateAsync(StudyCreateDto dto);
    Task UpdateAsync(long id, StudyUpdateDto dto);
    Task DeleteAsync(long id);
}
