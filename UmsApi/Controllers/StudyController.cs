using Microsoft.AspNetCore.Mvc;
using UmsApi.DTOs;
using UmsApi.DTOs.Study;
using UmsApi.Services;

namespace UmsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudiesController : ControllerBase
{
    private readonly IStudyService _service;

    public StudiesController(IStudyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponseDto<StudyResponseDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10,
        [FromQuery] bool showAll = false,
        [FromQuery] string? search = null
    )
    {
        var studies = await _service.GetAllAsync(page, limit, showAll, search);
        return Ok(studies);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<StudyResponseDto>> GetById(long id)
    {
        var study = await _service.GetByIdAsync(id);
        if (study == null)
            return NotFound();
        return Ok(study);
    }

    [HttpPost]
    public async Task<ActionResult<StudyResponseDto>> Create(StudyCreateDto dto)
    {
        var study = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = study.Id }, study);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, StudyUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
