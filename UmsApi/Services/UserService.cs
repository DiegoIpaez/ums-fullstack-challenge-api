using Microsoft.EntityFrameworkCore;
using UmsApi.DTOs;
using UmsApi.DTOs.Address;
using UmsApi.DTOs.User;
using UmsApi.Models;
using UmsApi.Repositories;

namespace UmsApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResponseDto<UserDto>> GetAllAsync(
        int page = 1,
        int limit = 10,
        bool showAll = false,
        string? search = null
    )
    {
        var query = _repository.Query();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(user => user.Name.Contains(search) || user.Email.Contains(search));
        }

        var totalItems = await query.CountAsync();

        if (!showAll)
        {
            query = query.Skip((page - 1) * limit).Take(limit);
        }

        var items = await query.Select(MapToDto()).ToListAsync();

        return new PaginatedResponseDto<UserDto>
        {
            Items = items,
            Page = showAll ? 1 : page,
            PageSize = showAll ? totalItems : limit,
            TotalItems = totalItems,
        };
    }

    public async Task<UserDto?> GetByIdAsync(long id)
    {
        return await _repository
            .Query()
            .Where(user => user.Id == id)
            .Select(MapToDto())
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(long id, UserUpdateDto request)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            return false;

        user.Name = request.Name;

        await _repository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            return false;

        await _repository.DeleteAsync(user);
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
