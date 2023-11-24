using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.WorkOrderDto;

namespace ServiceManagerApi.Controllers.Esms;

[Route("api/[controller]")]
[ApiController]
public class WorkOrdersController : ControllerBase
{
    private readonly EnpDBContext _context;
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;

    public WorkOrdersController(EnpDBContext context, IMemoryCache cache, IMapper mapper)
    {
        _context = context;
        _cache = cache;
        _mapper = mapper;
    }

    // GET: api/WorkOrders/tenant/{tenantId}
    [HttpGet("tenant/{tenantId}")]
    public async Task<ActionResult<IEnumerable<WorkOrderGetDto>>> GetWorkOrders(string tenantId)
    {
        if (_context.WorkOrders == null) return NotFound();

        // return await _context.WorkOrders.Where(order => order.TenantId == tenantId && order.CompletionDate == null)
        //     .ToListAsync();
        return _mapper.Map<List<WorkOrderGetDto>>(await _context.WorkOrders
            .Where(order => order.TenantId == tenantId && order.CompletionDate == null)
            .Include(w => w.Backlog)
            .ToListAsync());
    }

    //with pagination
    // GET: api/WorkOrders/tenant/{tenantId}/completed?pageNumber=1&pageSize=10
    [HttpGet("tenant/{tenantId}/completed")]
    public async Task<ActionResult<IEnumerable<WorkOrder>>> GetCompletedWorkOrders(string tenantId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize)
    {
        if (_context.WorkOrders == null) return NotFound();
        var workOrders = _context.WorkOrders.Where(order => order.TenantId == tenantId && order.CompletionDate != null);
        if (pageNumber.HasValue && pageSize.HasValue)
            workOrders = workOrders
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        return await workOrders.ToListAsync();
    }


    // GET: api/WorkOrders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkOrder>> GetWorkOrder(int id)
    {
        if (_context.WorkOrders == null) return NotFound();
        var workOrder = await _context.WorkOrders.FindAsync(id);

        if (workOrder == null) return NotFound();

        return workOrder;
    }

    // PUT: api/WorkOrders/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkOrder(int id, WorkOrder workOrder)
    {
        if (id != workOrder.Id) return BadRequest();

        _context.Entry(workOrder).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkOrderExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/WorkOrders
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WorkOrder>> PostWorkOrder(WorkOrder workOrder)
    {
        if (_context.WorkOrders == null) return Problem("Entity set 'EnpDbContext.WorkOrders'  is null.");
        _context.WorkOrders.Add(workOrder);
        await _context.SaveChangesAsync();

        //invalidate cache
        _cache.Remove($"backlogs");
        return CreatedAtAction("GetWorkOrder", new { id = workOrder.Id }, workOrder);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<WorkOrder> patchWorkOrder)
    {
        var workOrder = await _context.WorkOrders.FindAsync(id);

        if (workOrder == null) return BadRequest();

        patchWorkOrder.ApplyTo(workOrder, ModelState);

        await _context.SaveChangesAsync();
        _cache.Remove($"backlogs");
        return Ok(workOrder);
    }

    // DELETE: api/WorkOrders/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkOrder(int id)
    {
        if (_context.WorkOrders == null) return NotFound();
        var workOrder = await _context.WorkOrders.FindAsync(id);
        if (workOrder == null) return NotFound();

        _context.WorkOrders.Remove(workOrder);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkOrderExists(int id)
    {
        return (_context.WorkOrders?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
