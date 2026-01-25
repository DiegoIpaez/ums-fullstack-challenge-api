using Microsoft.EntityFrameworkCore;
using UmsApi.DTOs;
using UmsApi.DTOs.Study;
using UmsApi.Models;
using UmsApi.Repositories;

namespace UmsApi.Services;

public class StudyService : IStudyService
{
    private readonly IStudyRepository _repository;

    public StudyService(IStudyRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResponseDto<StudyResponseDto>> GetAllAsync(
        int page = 1,
        int limit = 10,
        bool showAll = false,
        string? search = null
    )
    {
        var query = _repository.Query();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(study =>
                study.Title.Contains(search) || study.Institution.Contains(search)
            );
        }

        var totalItems = await query.CountAsync();

        if (!showAll)
        {
            query = query.Skip((page - 1) * limit).Take(limit);
        }

        var items = await query
            .Select(study => new StudyResponseDto
            {
                Id = study.Id,
                Title = study.Title,
                Institution = study.Institution,
                StartDate = study.StartDate,
                EndDate = study.EndDate,
                UserId = study.UserId,
            })
            .ToListAsync();

        return new PaginatedResponseDto<StudyResponseDto>
        {
            Items = items,
            Page = showAll ? 1 : page,
            PageSize = showAll ? totalItems : limit,
            TotalItems = totalItems,
        };
    }

    public async Task<StudyResponseDto?> GetByIdAsync(long id)
    {
        var study = await _repository.GetByIdAsync(id);
        if (study == null)
            return null;

        return new StudyResponseDto
        {
            Id = study.Id,
            Title = study.Title,
            Institution = study.Institution,
            StartDate = study.StartDate,
            EndDate = study.EndDate,
            UserId = study.UserId,
        };
    }

    public async Task<StudyResponseDto> CreateAsync(StudyCreateDto dto)
    {
        var study = new Study
        {
            Title = dto.Title,
            Institution = dto.Institution,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            UserId = dto.UserId,
        };

        study = await _repository.AddAsync(study);

        return new StudyResponseDto
        {
            Id = study.Id,
            Title = study.Title,
            Institution = study.Institution,
            StartDate = study.StartDate,
            EndDate = study.EndDate,
            UserId = study.UserId,
        };
    }

    public async Task UpdateAsync(long id, StudyUpdateDto dto)
    {
        var study = await _repository.GetByIdAsync(id);
        if (study == null)
            throw new KeyNotFoundException("Study not found");

        study.Title = dto.Title;
        study.Institution = dto.Institution;
        study.StartDate = dto.StartDate;
        study.EndDate = dto.EndDate;

        await _repository.UpdateAsync(study);
    }

    public async Task DeleteAsync(long id)
    {
        var study = await _repository.GetByIdAsync(id);
        if (study == null)
            throw new KeyNotFoundException("Study not found");

        await _repository.DeleteAsync(study);
    }
}
