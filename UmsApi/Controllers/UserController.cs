using Microsoft.AspNetCore.Mvc;
using UmsApi.DTOs;
using UmsApi.DTOs.User;
using UmsApi.Services;

namespace UmsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponseDto<UserDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10,
        [FromQuery] bool showAll = false,
        [FromQuery] string? search = null
    )
    {
        var users = await _userService.GetAllAsync(page, limit, showAll, search);
        return Ok(users);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserDto>> GetById(long id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> Update(long id, UserUpdateDto request)
    {
        var updated = await _userService.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _userService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
