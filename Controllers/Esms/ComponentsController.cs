using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.Component;
using ServiceManagerApi.Dtos.Equipments;

namespace ServiceManagerApi.Controllers.Esms;

[Route("api/[controller]")]
[ApiController]
public class ComponentsController : BaeApiController<ComponentDto>
{
  private readonly EnpDBContext _context;

  public ComponentsController(EnpDBContext context)
  {
    _context = context;
  }

  // GET: api/Components
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Component>>> GetComponents()
  {
    if (_context.Components == null) return NotFound();
    return await _context.Components.ToListAsync();
  }

  // GET: api/Components/5
  [HttpGet("{id}")]
  public async Task<ActionResult<Component>> GetComponent(int id)
  {
    if (_context.Components == null) return NotFound();
    var component = await _context.Components.FindAsync(id);

    if (component == null) return NotFound();

    return component;
  }
    [HttpGet("tenant/{tenant}")]
    public async Task<ActionResult<List<Component>>> GetComponentByTenant(string tenant)
    {
        if (tenant == null)
        {
             return BadRequest("Tenant parameter is null."); 
        }
        if (_context.Components == null) { return BadRequest(); };
        var components = await _context.Components
    .Where(te => te.TenantId == tenant)
    .Select(e => new Component { 
        Id = e.Id,
        TenantId=e.TenantId
        ,Name=e.Name,
        ModelId=e.ModelId,
        ComponentClass=e.ComponentClass,
         Model= _context.Models
    .Where(te => te.ModelId == e.ModelId)
    .FirstOrDefault() ?? new Model { },
        ScheduledLifeHours =e.ScheduledLifeHours,
        ComponentClassNavigation = _context.CompomentClasses
    .Where(te => te.Id == e.ComponentClass)
    .FirstOrDefault() ?? new CompomentClass { }

    })
    .ToListAsync();

     

        return components;
    }

    // PUT: api/Components/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
  public async Task<IActionResult> PutComponent(int id, Component component)
  {
    if (id != component.Id) return BadRequest();

    _context.Entry(component).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!ComponentExists(id))
        return NotFound();
      else
        throw;
    }

    return NoContent();
  }

  // POST: api/Components
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPost]
  public async Task<ActionResult<Component>> PostComponent(ComponentDto component)
  {
        if (_context.Components == null) { return Problem("Entity set 'EnpDBContext.Components'  is null."); }
        var componentMapper = _mapper.Map<Component>(component);
       _context.Components.Add(componentMapper);
       await _context.SaveChangesAsync();

    return CreatedAtAction("GetComponent", new { id = componentMapper.Id }, componentMapper);
  }

  // DELETE: api/Components/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteComponent(int id)
  {
        
    if (_context.Components == null) return NotFound();
    var component = await _context.Components.FindAsync(id);
    if (component == null) return NotFound();

    _context.Components.Remove(component);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool ComponentExists(int id)
  {
    return (_context.Components?.Any(e => e.Id == id)).GetValueOrDefault();
  }
}