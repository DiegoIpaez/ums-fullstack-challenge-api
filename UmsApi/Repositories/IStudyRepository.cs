using UmsApi.Models;

namespace UmsApi.Repositories;

public interface IStudyRepository
{
    IQueryable<Study> Query();
    Task<List<Study>> GetAllAsync();
    Task<Study?> GetByIdAsync(long id);
    Task<Study> AddAsync(Study study);
    Task UpdateAsync(Study study);
    Task DeleteAsync(Study study);
}
