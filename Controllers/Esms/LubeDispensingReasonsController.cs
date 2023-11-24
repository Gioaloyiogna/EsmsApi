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
public class LubeDispensingReasonsController : ControllerBase
{
    private readonly EnpDBContext _context;

    public LubeDispensingReasonsController(EnpDBContext context)
    {
        _context = context;
    }

    // GET: api/LubeDispensingReasons
    [HttpGet("tenant/{tenantId}")]
    public async Task<ActionResult<IEnumerable<LubeDispensingReason>>> GetLubeDispensingReasons(string tenantId)
    {
        if (_context.LubeDispensingReasons == null) return NotFound();
        return await _context.LubeDispensingReasons
            .Where(lubeDispensingReason => lubeDispensingReason.TenantId == tenantId)
            .ToListAsync();
    }

    // GET: api/LubeDispensingReasons/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LubeDispensingReason>> GetLubeDispensingReason(int id)
    {
        if (_context.LubeDispensingReasons == null) return NotFound();
        var lubeDispensingReason = await _context.LubeDispensingReasons.FindAsync(id);

        if (lubeDispensingReason == null) return NotFound();

        return lubeDispensingReason;
    }

    // PUT: api/LubeDispensingReasons/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLubeDispensingReason(int id, LubeDispensingReason lubeDispensingReason)
    {
        if (id != lubeDispensingReason.Id) return BadRequest();

        _context.Entry(lubeDispensingReason).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LubeDispensingReasonExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/LubeDispensingReasons
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<LubeDispensingReason>> PostLubeDispensingReason(
        LubeDispensingReason lubeDispensingReason)
    {
        if (_context.LubeDispensingReasons == null)
            return Problem("Entity set 'EnpDbContext.LubeDispensingReasons'  is null.");
        _context.LubeDispensingReasons.Add(lubeDispensingReason);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLubeDispensingReason", new { id = lubeDispensingReason.Id }, lubeDispensingReason);
    }

    // DELETE: api/LubeDispensingReasons/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLubeDispensingReason(int id)
    {
        if (_context.LubeDispensingReasons == null) return NotFound();
        var lubeDispensingReason = await _context.LubeDispensingReasons.FindAsync(id);
        if (lubeDispensingReason == null) return NotFound();

        _context.LubeDispensingReasons.Remove(lubeDispensingReason);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LubeDispensingReasonExists(int id)
    {
        return (_context.LubeDispensingReasons?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
