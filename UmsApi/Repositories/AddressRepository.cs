using Microsoft.EntityFrameworkCore;
using UmsApi.Data;
using UmsApi.Models;

namespace UmsApi.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Address> Query() => _context.Addresses;

    public async Task<List<Address>> GetAllAsync() => await _context.Addresses.ToListAsync();

    public async Task<Address?> GetByIdAsync(long id) => await _context.Addresses.FindAsync(id);

    public async Task<Address> AddAsync(Address address)
    {
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task UpdateAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Address address)
    {
        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
    }
}
