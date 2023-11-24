using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;

namespace ServiceManagerApi.Controllers.Esms
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentPlanController : ControllerBase
    {
        private readonly EnpDBContext _context;
        public ComponentPlanController(EnpDBContext context)
        {
            _context = context;
        }


        [HttpGet("tenant/{tenantId}")]
        public Task<List<ComponentPlan>> GetAllComponentsPlan(string tenantId)
        {
            var componentPlan = _context.ComponentPlans.Where(leav => leav.TenantId == tenantId).ToListAsync();

            return componentPlan;
            //try
            //{
            //    if (string.IsNullOrWhiteSpace(tenantId))
            //    {
            //        var components = _context.CompomentClasses.Where(leav => leav.TenantId == tenantId).ToListAsync();
            //        if (components != null)
            //        {
            //            return components;

            //        }

            //    }
            //    else
            //    {

            //    }

            //}
            //catch (Exception ex)
            //{
            //    ex= new Exception("Error happened while getting components");
            //    throw ex;
            //}
        }

        //get individual component condition
        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentPlan>> GetComponentPlan(int id)
        {
            if (_context.ComponentPlans == null)
            {
                return NotFound();
            }
            var component = await _context.ComponentPlans.FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return component;
        }

        //posting componenent condition
        [HttpPost]
        public async Task<ActionResult<ComponentPlan>> PostComponentClass(ComponentPlan component)
        {
            if (_context.ComponentPlans == null)
            {
                return Problem("Entity set 'EnpDBContext.Categories'  is null.");
            }
            _context.ComponentPlans.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponentPlan", new { id = component.Id }, component);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentPlan(int id, ComponentPlan component)
        {
            if (id != component.Id)
            {
                return BadRequest();
            }

            _context.Entry(component).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentPlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool ComponentPlanExists(int id)
        {
            return (_context.ComponentPlans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponentPlan(int id)
        {
            if (_context.ComponentPlans == null)
            {
                return NotFound();
            }
            var componentPlan = await _context.ComponentPlans.FindAsync(id);
            if (componentPlan == null)
            {
                return NotFound();
            }

            _context.ComponentPlans.Remove(componentPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }

}
