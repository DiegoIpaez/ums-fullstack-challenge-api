using UmsApi.DTOs;
using UmsApi.DTOs.Address;

namespace UmsApi.Services;

public interface IAddressService
{
    Task<PaginatedResponseDto<AddressDto>> GetAllAsync(
        int page = 1,
        int limit = 10,
        bool showAll = false,
        string? search = null
    );
    Task<AddressDto?> GetByIdAsync(long id);
    Task<AddressDto> CreateAsync(AddressCreateDto dto, long userId);
    Task<bool> UpdateAsync(long id, AddressUpdateDto dto, long userId, bool isAdmin);
    Task<bool> DeleteAsync(long id, long userId, bool isAdmin);
}
