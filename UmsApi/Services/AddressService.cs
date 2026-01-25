using Microsoft.EntityFrameworkCore;
using UmsApi.DTOs;
using UmsApi.DTOs.Address;
using UmsApi.Models;
using UmsApi.Repositories;

namespace UmsApi.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _repository;

    public AddressService(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResponseDto<AddressDto>> GetAllAsync(
        int page = 1,
        int limit = 10,
        bool showAll = false,
        string? search = null
    )
    {
        var query = _repository.Query();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.ToLower();
            query = query.Where(address =>
                address.Street.ToLower().Contains(term)
                || address.City.ToLower().Contains(term)
                || address.State.ToLower().Contains(term)
                || address.Country.ToLower().Contains(term)
            );
        }

        var totalItems = await query.CountAsync();

        if (!showAll)
        {
            query = query.Skip((page - 1) * limit).Take(limit);
        }

        var items = await query.ToListAsync();

        return new PaginatedResponseDto<AddressDto>
        {
            Items = items.Select(MapToDto).ToList(),
            Page = page,
            PageSize = limit,
            TotalItems = totalItems,
        };
    }

    public async Task<AddressDto?> GetByIdAsync(long id)
    {
        var address = await _repository.GetByIdAsync(id);
        return address == null ? null : MapToDto(address);
    }

    public async Task<AddressDto> CreateAsync(AddressCreateDto dto, long userId)
    {
        var address = new Address
        {
            Street = dto.Street,
            City = dto.City,
            State = dto.State,
            Country = dto.Country,
            PostalCode = dto.PostalCode,
            UserId = userId,
        };
        address = await _repository.AddAsync(address);
        return MapToDto(address);
    }

    public async Task<bool> UpdateAsync(long id, AddressUpdateDto dto, long userId, bool isAdmin)
    {
        var address = await _repository.GetByIdAsync(id);
        if (address == null)
            return false;
        if (!isAdmin && address.UserId != userId)
            throw new UnauthorizedAccessException();
        address.Street = dto.Street;
        address.City = dto.City;
        address.State = dto.State;
        address.Country = dto.Country;
        address.PostalCode = dto.PostalCode;
        await _repository.UpdateAsync(address);
        return true;
    }

    public async Task<bool> DeleteAsync(long id, long userId, bool isAdmin)
    {
        var address = await _repository.GetByIdAsync(id);
        if (address == null)
            return false;
        if (!isAdmin && address.UserId != userId)
            throw new UnauthorizedAccessException();
        await _repository.DeleteAsync(address);
        return true;
    }

    private static AddressDto MapToDto(Address address)
    {
        return new AddressDto
        {
            Id = address.Id,
            Street = address.Street,
            City = address.City,
            State = address.State,
            Country = address.Country,
            PostalCode = address.PostalCode,
            UserId = address.UserId,
        };
    }
}
