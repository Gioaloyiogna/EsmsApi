using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;

namespace ServiceManagerApi.Controllers.Esms;

[Route("api/[controller]")]
[ApiController]
public class BacklogStatusesController : ControllerBase
{
    private readonly EnpDBContext _context;

    public BacklogStatusesController(EnpDBContext context)
    {
        _context = context;
    }

    // GET: api/BacklogStatuses
    [HttpGet("tenant/{tenantId}")]
    public async Task<ActionResult<IEnumerable<BacklogStatus>>> GetBacklogStatuses(string TenantId)
    {
        if (_context.BacklogStatuses == null) return NotFound();
        return await _context.BacklogStatuses.Where(backlogStatus => backlogStatus.TenantId == TenantId).ToListAsync();
    }

    // GET: api/BacklogStatuses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BacklogStatus>> GetBacklogStatus(int id)
    {
        if (_context.BacklogStatuses == null) return NotFound();
        var backlogStatus = await _context.BacklogStatuses.FindAsync(id);

        if (backlogStatus == null) return NotFound();

        return backlogStatus;
    }

    // PUT: api/BacklogStatuses/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBacklogStatus(int id, BacklogStatus backlogStatus)
    {
        if (id != backlogStatus.Id) return BadRequest();

        _context.Entry(backlogStatus).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BacklogStatusExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/BacklogStatuses
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<BacklogStatus>> PostBacklogStatus(BacklogStatus backlogStatus)
    {
        if (_context.BacklogStatuses == null) return Problem("Entity set 'EnpDbContext.BacklogStatuses'  is null.");
        _context.BacklogStatuses.Add(backlogStatus);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBacklogStatus", new { id = backlogStatus.Id }, backlogStatus);
    }

    // DELETE: api/BacklogStatuses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBacklogStatus(int id)
    {
        if (_context.BacklogStatuses == null) return NotFound();
        var backlogStatus = await _context.BacklogStatuses.FindAsync(id);
        if (backlogStatus == null) return NotFound();

        _context.BacklogStatuses.Remove(backlogStatus);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool BacklogStatusExists(int id)
    {
        return (_context.BacklogStatuses?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
