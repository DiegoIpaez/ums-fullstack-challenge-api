using UmsApi.DTOs;
using UmsApi.DTOs.User;

namespace UmsApi.Services;

public interface IUserService
{
    Task<PaginatedResponseDto<UserDto>> GetAllAsync(
        int page = 1,
        int limit = 10,
        bool showAll = false,
        string? search = null
    );
    Task<UserDto?> GetByIdAsync(long id);
    Task<bool> UpdateAsync(long id, UserUpdateDto request);
    Task<bool> DeleteAsync(long id);
}
