using UmsApi.DTOs;

namespace UmsApi.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(long id);
    Task<bool> UpdateAsync(long id, UpdateUserRequest request);
    Task<bool> DeleteAsync(long id);
}
