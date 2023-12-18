using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.Component;
using ServiceManagerApi.Dtos.Equipments;
using ServiceManagerApi.Dtos.Transfer;

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

    [HttpGet("tracker/{id}")]
    public async Task<ActionResult<Tracker>> GetTracker(int id)
    {
        if (_context.Trackers == null) return NotFound();
        var tracker = await _context.Trackers.FindAsync(id);

        if (tracker == null) return NotFound();

        return tracker;
    }
    [HttpGet("tenant/tracker/{tenant}")]
    public async Task<ActionResult<List<Tracker>>> GetTrackerByTenant(string tenant)
    {
        if (tenant == null)
        {
            return BadRequest("Tenant parameter is null.");
        }
        if (_context.Trackers == null) { return BadRequest(); };
        var trackers = await _context.Trackers
    .Where(te => te.TenantId == tenant)
    .Select(e => new Tracker
    {
        Id = e.Id,
        TenantId = e.TenantId
        ,
        Date=e.Date,
        ComponentName = e.ComponentName,
        ModelId = e.ModelId,
        ComponentSerialNo = e.ComponentSerialNo,
        Model = _context.Models
    .Where(te => te.ModelId == e.ModelId)
    .FirstOrDefault() ?? new Model { },
        Condition=_context.ComponentConditions.Where(te=>te.Id==e.ConditionId).FirstOrDefault()?? new ComponentCondition { },
        ConditionId = e.ConditionId,
        Plan=_context.ComponentPlans.Where(te=>te.Id==e.PlanId).FirstOrDefault()?? new ComponentPlan { },
        PlanId=e.PlanId,
        Reference=e.Reference,
        Details=e.Details,
        Value=e.Value,
        Location=_context.Locations.Where(te=>te.Id==e.LocationId).FirstOrDefault()?? new Location { },
        LocationId=e.LocationId,
        ComponentId=e.ComponentId,
       

    })
    .ToListAsync();



        return trackers;
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
    [HttpPost("tracker")]
    public async Task<ActionResult<Transfer>> postTracker(TrackerDto tracker)
    {
        String? referenceId = null;
        if (tracker != null)
        {


            var trackerCount = _context.Trackers.Where(te => te.TenantId == "tarkwa").Count();
            var getCountString = trackerCount.ToString().Length;
            switch (getCountString)
            {
                case 1:
                    referenceId = "TA" + "00000" + trackerCount;
                    break;
                case 2:
                    referenceId = "TH" + "000" + trackerCount;
                    break;
                case 3:
                    referenceId = "TH" + "00" + trackerCount;
                    break;
                case 4:
                    referenceId = "TH" + "0" + trackerCount;
                    break;
                default:
                    referenceId = "TH" + trackerCount;
                    break;


            };
            tracker.ComponentId = referenceId;
           var mappedData=_mapper.Map<Tracker>(tracker);
            _context.Trackers.Add(mappedData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrackerExists(tracker.Id))
                    return Conflict();
                else
                    throw;
            }

            return CreatedAtAction("GetTracker", new { id = tracker.Id }, tracker);

        }
        return Problem("Tracker data object is null");





    }


    // patch omponents
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PatchComponent(int id, [FromBody] JsonPatchDocument<Component> component)
    {
        
            try
            {
                var existingComponent = await _context.Components.FindAsync(id);

                if ( existingComponent == null) return BadRequest();

                component.ApplyTo(existingComponent, ModelState);

                await _context.SaveChangesAsync();
        


                return Ok();
            }
            catch (DbUpdateException ex)
            {

                
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rollback the transaction if needed
                
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var componentDelete = await _context.Components.FindAsync(id);
        if (componentDelete == null) return NotFound();
        _context.Components.Remove(componentDelete);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    private bool ComponentExists(int id)
  {
    return (_context.Components?.Any(e => e.Id == id)).GetValueOrDefault();
  }
    private bool TrackerExists(int id)
    {
        return (_context.Trackers?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}