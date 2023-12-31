using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Controllers.Esms;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.ProdPump;

namespace ServiceManagerApi.Controllers.Production;

public class ProPumpController : BaeApiController<ProPumpController>
{
    private readonly EnpDBContext _context;

    public ProPumpController(EnpDBContext context)
    {
        _context = context;
    }

    // GET: api/ProPump
    [HttpGet("tenant/{tenantId}")]
    public Task<List<ProductionPump>> GetProductionPumps(string tenantId)
    {
        var proPump = _context.ProductionPumps.Where(pump => pump.TenantId == tenantId).ToListAsync();
        return proPump;
    }


    [HttpGet("id")]
    [ProducesResponseType(typeof(ProdRawMaterial), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var productionPump = await _context.ProductionPumps.FindAsync(id);
        if (productionPump == null) return NotFound();

        return Ok(productionPump);
    }

    // PUT: api/ProPump/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutProductionPump(int id, ProductionPump productionPump)
    {
        if (id != productionPump.Id) return BadRequest();

        _context.Entry(productionPump).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductionPumpExists(productionPump.Name)) return NotFound();

            throw;
        }

        return NoContent();
    }

    // POST: api/ProPump
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(typeof(ProductionPump), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(ProdPumpPostDto prodPumpPostDto)
    {
        var productionPump = _mapper.Map<ProductionPump>(prodPumpPostDto);
        if (ProductionPumpExists(productionPump.Name)) return Conflict();

        _context.ProductionPumps.Add(productionPump);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = productionPump.Id }, productionPump);
    }

    // DELETE: api/ProPump/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProductionPump(int id)
    {
        var productionPump = await _context.ProductionPumps.FindAsync(id);
        if (productionPump == null) return NotFound();

        _context.ProductionPumps.Remove(productionPump);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductionPumpExists(string name)
    {
        return _context.ProductionPumps.Any(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
    }
}
