using Microsoft.EntityFrameworkCore;
using UmsApi.Data;
using UmsApi.Models;

namespace UmsApi.Repositories;

public class StudyRepository : IStudyRepository
{
    private readonly AppDbContext _context;

    public StudyRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Study> Query() => _context.Studies;

    public async Task<List<Study>> GetAllAsync() => await _context.Studies.ToListAsync();

    public async Task<Study?> GetByIdAsync(long id) => await _context.Studies.FindAsync(id);

    public async Task<Study> AddAsync(Study study)
    {
        _context.Studies.Add(study);
        await _context.SaveChangesAsync();
        return study;
    }

    public async Task UpdateAsync(Study study)
    {
        _context.Studies.Update(study);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Study study)
    {
        _context.Studies.Remove(study);
        await _context.SaveChangesAsync();
    }
}
