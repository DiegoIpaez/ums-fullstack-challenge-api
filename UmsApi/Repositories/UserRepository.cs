using Microsoft.EntityFrameworkCore;
using UmsApi.Data;
using UmsApi.Models;

namespace UmsApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<User> Query() => _context.Users.Where(user => !user.Deleted);

    public async Task<List<User>> GetAllAsync() =>
        await _context.Users.Where(user => !user.Deleted).ToListAsync();

    public async Task<User?> GetByIdAsync(long id) =>
        await _context.Users.FirstOrDefaultAsync(user => user.Id == id && !user.Deleted);

    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        user.Deleted = true;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
