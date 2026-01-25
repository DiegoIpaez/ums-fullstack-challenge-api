using UmsApi.Models;

namespace UmsApi.Repositories;

public interface IUserRepository
{
    IQueryable<User> Query();
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(long id);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}
