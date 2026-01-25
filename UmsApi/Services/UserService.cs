using Microsoft.EntityFrameworkCore;
using UmsApi.Data;
using UmsApi.DTOs;
using UmsApi.DTOs.User;
using UmsApi.Models;

namespace UmsApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        return await _context.Users.Where(user => !user.Deleted).Select(MapToDto()).ToListAsync();
    }

    public async Task<UserDto?> GetByIdAsync(long id)
    {
        return await _context
            .Users.Where(user => user.Id == id && !user.Deleted)
            .Select(MapToDto())
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(long id, UserUpdateDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id && !user.Deleted);

        if (user == null)
            return false;

        user.Name = request.Name;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id && !user.Deleted);

        if (user == null)
            return false;

        user.Deleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    private static System.Linq.Expressions.Expression<Func<User, UserDto>> MapToDto()
    {
        return user => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Addresses = user
                .Addresses.Select(a => new AddressDto
                {
                    Id = a.Id,
                    Street = a.Street,
                    City = a.City,
                    State = a.State,
                    Country = a.Country,
                    PostalCode = a.PostalCode,
                })
                .ToList(),
            Studies = user
                .Studies.Select(s => new StudyDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                })
                .ToList(),
        };
    }
}
