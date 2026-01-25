using Microsoft.EntityFrameworkCore;
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

    public async Task<List<AddressDto>> GetAllAsync()
    {
        var addresses = await _repository.GetAllAsync();
        return addresses.Select(MapToDto).ToList();
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
