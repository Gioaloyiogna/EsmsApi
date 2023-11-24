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
public class LubeDispensingSourcesController : ControllerBase
{
    private readonly EnpDBContext _context;

    public LubeDispensingSourcesController(EnpDBContext context)
    {
        _context = context;
    }

    // GET: api/LubeDispensingSources
    [HttpGet("tenant/{tenantId}")]
    public async Task<ActionResult<IEnumerable<LubeDispensingSource>>> GetLubeDispensingSources(string tenantId)
    {
        if (_context.LubeDispensingSources == null) return NotFound();
        return await _context.LubeDispensingSources
            .Where(lubeDispensingSource => lubeDispensingSource.TenantId == tenantId)
            .ToListAsync();
    }

    // GET: api/LubeDispensingSources/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LubeDispensingSource>> GetLubeDispensingSource(int id)
    {
        if (_context.LubeDispensingSources == null) return NotFound();
        var lubeDispensingSource = await _context.LubeDispensingSources.FindAsync(id);

        if (lubeDispensingSource == null) return NotFound();

        return lubeDispensingSource;
    }

    // PUT: api/LubeDispensingSources/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLubeDispensingSource(int id, LubeDispensingSource lubeDispensingSource)
    {
        if (id != lubeDispensingSource.Id) return BadRequest();

        _context.Entry(lubeDispensingSource).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LubeDispensingSourceExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/LubeDispensingSources
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<LubeDispensingSource>> PostLubeDispensingSource(
        LubeDispensingSource lubeDispensingSource)
    {
        if (_context.LubeDispensingSources == null)
            return Problem("Entity set 'EnpDbContext.LubeDispensingSources'  is null.");
        _context.LubeDispensingSources.Add(lubeDispensingSource);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLubeDispensingSource", new { id = lubeDispensingSource.Id }, lubeDispensingSource);
    }

    // DELETE: api/LubeDispensingSources/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLubeDispensingSource(int id)
    {
        if (_context.LubeDispensingSources == null) return NotFound();
        var lubeDispensingSource = await _context.LubeDispensingSources.FindAsync(id);
        if (lubeDispensingSource == null) return NotFound();

        _context.LubeDispensingSources.Remove(lubeDispensingSource);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LubeDispensingSourceExists(int id)
    {
        return (_context.LubeDispensingSources?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
