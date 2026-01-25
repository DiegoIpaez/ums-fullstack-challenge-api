using UmsApi.Models;

namespace UmsApi.Repositories;

public interface IAddressRepository
{
    IQueryable<Address> Query();
    Task<List<Address>> GetAllAsync();
    Task<Address?> GetByIdAsync(long id);
    Task<Address> AddAsync(Address address);
    Task UpdateAsync(Address address);
    Task DeleteAsync(Address address);
}
