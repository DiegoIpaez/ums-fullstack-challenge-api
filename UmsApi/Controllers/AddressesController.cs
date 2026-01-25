using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UmsApi.DTOs.Address;
using UmsApi.Extensions;
using UmsApi.Services;

namespace UmsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _service;

    public AddressesController(IAddressService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<AddressDto>>> GetAll()
    {
        var addresses = await _service.GetAllAsync();
        return Ok(addresses);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<AddressDto>> GetById(long id)
    {
        var address = await _service.GetByIdAsync(id);
        if (address == null)
            return NotFound();
        return Ok(address);
    }

    [HttpPost]
    public async Task<ActionResult<AddressDto>> Create(AddressCreateDto dto)
    {
        var userId = User.GetUserId();
        var address = await _service.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = address.Id }, address);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, AddressUpdateDto dto)
    {
        var userId = User.GetUserId();
        var isAdmin = User.IsAdmin();
        var updated = await _service.UpdateAsync(id, dto, userId, isAdmin);
        if (!updated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var userId = User.GetUserId();
        var isAdmin = User.IsAdmin();
        var deleted = await _service.DeleteAsync(id, userId, isAdmin);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}
