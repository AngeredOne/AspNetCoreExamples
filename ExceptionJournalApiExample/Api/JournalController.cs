using Asp.Versioning;
using AutoMapper;
using ExceptionJournalApiExample.Domain.Models.Api;
using ExceptionJournalApiExample.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExceptionJournalApiExample.Api;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/journal")]
public class JournalController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public JournalController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpPost("getRange")]
    public async Task<ActionResult<PaginatedResult<ExceptionJournalInfoApi>>> GetRange([FromQuery] int skip, [FromQuery] int take, [FromBody] JournalFilterApi? filter)
    {
        var query = _db.ExceptionJournals.AsQueryable();

        if (filter != null)
        {
            if (filter.From.HasValue)
                query = query.Where(x => x.CreatedAt >= filter.From.Value);
            if (filter.To.HasValue)
                query = query.Where(x => x.CreatedAt <= filter.To.Value);
            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(x => x.StackTrace.Contains(filter.Search));
        }

        var total = await query.CountAsync();
        var items = await query.OrderByDescending(x => x.CreatedAt)
            .Skip(skip)
            .Take(take)
            .Select(x => _mapper.Map<ExceptionJournalInfoApi>(x))
            .ToListAsync();

        return Ok(new PaginatedResult<ExceptionJournalInfoApi>
        {
            Skip = skip,
            Count = total,
            Items = items
        });
    }

    [HttpPost("getSingle")]
    public async Task<ActionResult<ExceptionJournalApi>> GetSingle([FromQuery] long id)
    {
        var journal = await _db.ExceptionJournals.FindAsync(id);
        if (journal == null) return NotFound();

        return Ok(_mapper.Map<ExceptionJournalApi>(journal));
    }
}