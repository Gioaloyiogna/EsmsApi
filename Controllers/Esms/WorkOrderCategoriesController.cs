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
public class WorkOrderCategoriesController : ControllerBase
{
    private readonly EnpDBContext _context;

    public WorkOrderCategoriesController(EnpDBContext context)
    {
        _context = context;
    }

    // GET: api/WorkOrderCategories
    [HttpGet("tenant/{tenantId}")]
    public async Task<ActionResult<IEnumerable<WorkOrderCategory>>> GetWorkOrderCategories(string tenantId)
    {
        if (_context.WorkOrderCategories == null) return NotFound();
        return await _context.WorkOrderCategories.Where((category) => category.TenantId == tenantId).ToListAsync();
    }

    // GET: api/WorkOrderCategories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkOrderCategory>> GetWorkOrderCategory(int id)
    {
        if (_context.WorkOrderCategories == null) return NotFound();
        var workOrderCategory = await _context.WorkOrderCategories.FindAsync(id);

        if (workOrderCategory == null) return NotFound();

        return workOrderCategory;
    }

    // PUT: api/WorkOrderCategories/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkOrderCategory(int id, WorkOrderCategory workOrderCategory)
    {
        if (id != workOrderCategory.Id) return BadRequest();

        _context.Entry(workOrderCategory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkOrderCategoryExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/WorkOrderCategories
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WorkOrderCategory>> PostWorkOrderCategory(WorkOrderCategory workOrderCategory)
    {
        if (_context.WorkOrderCategories == null)
            return Problem("Entity set 'EnpDbContext.WorkOrderCategories'  is null.");
        _context.WorkOrderCategories.Add(workOrderCategory);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetWorkOrderCategory", new { id = workOrderCategory.Id }, workOrderCategory);
    }

    // DELETE: api/WorkOrderCategories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkOrderCategory(int id)
    {
        if (_context.WorkOrderCategories == null) return NotFound();
        var workOrderCategory = await _context.WorkOrderCategories.FindAsync(id);
        if (workOrderCategory == null) return NotFound();

        _context.WorkOrderCategories.Remove(workOrderCategory);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkOrderCategoryExists(int id)
    {
        return (_context.WorkOrderCategories?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
