using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;

namespace ServiceManagerApi.Controllers.Esms;

[Route("api/[controller]")]
[ApiController]
public class WorkOrderTypesController : ControllerBase
{
    private readonly EnpDBContext _context;

    public WorkOrderTypesController(EnpDBContext context)
    {
        _context = context;
    }

    // GET: api/WorkOrderTypes/tentant/{tenantId}
    [HttpGet("tenant/{tenantId}")]
    public async Task<ActionResult<IEnumerable<WorkOrderType>>> GetWorkOrderTypes(string tenantId)
    {
        if (_context.WorkOrderTypes == null) return NotFound();
        return await _context.WorkOrderTypes.Where((type) => type.TenantId == tenantId).ToListAsync();
    }

    // GET: api/WorkOrderTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkOrderType>> GetWorkOrderType(int id)
    {
        if (_context.WorkOrderTypes == null) return NotFound();
        var workOrderType = await _context.WorkOrderTypes.FindAsync(id);

        if (workOrderType == null) return NotFound();

        return workOrderType;
    }

    // PUT: api/WorkOrderTypes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkOrderType(int id, WorkOrderType workOrderType)
    {
        if (id != workOrderType.Id) return BadRequest();

        _context.Entry(workOrderType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkOrderTypeExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/WorkOrderTypes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WorkOrderType>> PostWorkOrderType(WorkOrderType workOrderType)
    {
        if (_context.WorkOrderTypes == null) return Problem("Entity set 'EnpDbContext.WorkOrderTypes'  is null.");
        _context.WorkOrderTypes.Add(workOrderType);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetWorkOrderType", new { id = workOrderType.Id }, workOrderType);
    }

    // DELETE: api/WorkOrderTypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkOrderType(int id)
    {
        if (_context.WorkOrderTypes == null) return NotFound();
        var workOrderType = await _context.WorkOrderTypes.FindAsync(id);
        if (workOrderType == null) return NotFound();

        _context.WorkOrderTypes.Remove(workOrderType);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkOrderTypeExists(int id)
    {
        return (_context.WorkOrderTypes?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
