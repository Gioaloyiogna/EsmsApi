using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;

namespace ServiceManagerApi.Controllers.Esms
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentConditionController : ControllerBase
    {
        private readonly EnpDBContext _context;
        public ComponentConditionController(EnpDBContext context)
        {
            _context = context;
        }

        // Getting component condition
        [HttpGet("tenant/{tenantId}")]
        public Task<List<ComponentCondition>> GetAllComponents(string tenantId)
        {
            var components = _context.ComponentConditions.Where(leav => leav.TenantId == tenantId).ToListAsync();

            return components;
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
        public async Task<ActionResult<ComponentCondition>> GetComponentCondition(int id)
        {
            if (_context.ComponentConditions == null)
            {
                return NotFound();
            }
            var component = await _context.ComponentConditions.FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return component;
        }

        //posting componenent condition
        [HttpPost]
        public async Task<ActionResult<ComponentCondition>> PostComponentClass(ComponentCondition component)
        {
            if (_context.ComponentConditions == null)
            {
                return Problem("Entity set 'EnpDBContext.Categories'  is null.");
            }
            _context.ComponentConditions.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponentCondition", new { id = component.Id }, component);
        }


        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentCondition(int id, ComponentCondition component)
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
                if (!ComponentConditionExists(id))
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
        private bool ComponentConditionExists(int id)
        {
            return (_context.ComponentConditions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponentCondition(int id)
        {
            if (_context.ComponentConditions == null)
            {
                return NotFound();
            }
            var componentCondition = await _context.ComponentConditions.FindAsync(id);
            if (componentCondition == null)
            {
                return NotFound();
            }

            _context.ComponentConditions.Remove(componentCondition);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
