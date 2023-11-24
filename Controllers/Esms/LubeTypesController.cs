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
public class LubeTypesController : ControllerBase
{
    private readonly EnpDBContext _context;

    public LubeTypesController(EnpDBContext context)
    {
        _context = context;
    }

    // GET: api/LubeTypes/tenant/{tenantId}
    [HttpGet("tenant/{tenantId}")]
    public async Task<ActionResult<IEnumerable<LubeType>>> GetLubeTypes(string tenantId)
    {
        if (_context.LubeTypes == null) return NotFound();
        return await _context.LubeTypes.Where(lubeType => lubeType.TenantId == tenantId)
            .ToListAsync();
    }

    // GET: api/LubeTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LubeType>> GetLubeType(int id)
    {
        if (_context.LubeTypes == null) return NotFound();
        var lubeType = await _context.LubeTypes.FindAsync(id);

        if (lubeType == null) return NotFound();

        return lubeType;
    }

    // PUT: api/LubeTypes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLubeType(int id, LubeType lubeType)
    {
        if (id != lubeType.Id) return BadRequest();

        _context.Entry(lubeType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LubeTypeExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/LubeTypes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<LubeType>> PostLubeType(LubeType lubeType)
    {
        if (_context.LubeTypes == null) return Problem("Entity set 'EnpDbContext.LubeTypes'  is null.");
        _context.LubeTypes.Add(lubeType);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLubeType", new { id = lubeType.Id }, lubeType);
    }

    // DELETE: api/LubeTypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLubeType(int id)
    {
        if (_context.LubeTypes == null) return NotFound();
        var lubeType = await _context.LubeTypes.FindAsync(id);
        if (lubeType == null) return NotFound();

        _context.LubeTypes.Remove(lubeType);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LubeTypeExists(int id)
    {
        return (_context.LubeTypes?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
