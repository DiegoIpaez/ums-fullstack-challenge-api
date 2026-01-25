using UmsApi.DTOs.Address;

namespace UmsApi.Services;

public interface IAddressService
{
    Task<List<AddressDto>> GetAllAsync();
    Task<AddressDto?> GetByIdAsync(long id);
    Task<AddressDto> CreateAsync(AddressCreateDto dto, long userId);
    Task<bool> UpdateAsync(long id, AddressUpdateDto dto, long userId, bool isAdmin);
    Task<bool> DeleteAsync(long id, long userId, bool isAdmin);
}
