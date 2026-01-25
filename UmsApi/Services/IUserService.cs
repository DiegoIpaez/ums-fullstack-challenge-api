using UmsApi.DTOs.User;

namespace UmsApi.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(long id);
    Task<bool> UpdateAsync(long id, UserUpdateDto request);
    Task<bool> DeleteAsync(long id);
}
