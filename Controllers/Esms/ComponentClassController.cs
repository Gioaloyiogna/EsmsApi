using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using System.ComponentModel;
using System.Data;

namespace ServiceManagerApi.Controllers.Esms
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentClassController : ControllerBase
    {
        private readonly EnpDBContext _context;
        public ComponentClassController(EnpDBContext context)
        {
            _context = context;
        }
        
        [HttpGet("tenant/{tenantId}")]
        public Task<List<CompomentClass>> GetAllComponents(string tenantId)
        {
            var components = _context.CompomentClasses.Where(leav => leav.TenantId == tenantId).ToListAsync();

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
       

        //get individual componennt Class
        [HttpGet("{id}")]
        public async Task<ActionResult<CompomentClass>> GetComponentClass(int id)
        {
            if (_context.CompomentClasses== null)
            {
                return NotFound();
            }
            var component = await _context.CompomentClasses.FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return component;
        }

        //posting componenent class
        [HttpPost]
        public async Task<ActionResult<CompomentClass>> PostComponentClass(CompomentClass component)
        {
            if (_context.CompomentClasses == null)
            {
                return Problem("Entity set 'EnpDBContext.Categories'  is null.");
            }
            _context.CompomentClasses.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponentClass", new { id = component.Id }, component);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentClass(int id, CompomentClass component)
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
                if (!ComponentClassExists(id))
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
        private bool ComponentClassExists(int id)
        {
            return (_context.CompomentClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponentClass(int id)
        {
            if (_context.CompomentClasses == null)
            {
                return NotFound();
            }
            var componentClass = await _context.CompomentClasses.FindAsync(id);
            if (componentClass == null)
            {
                return NotFound();
            }

            _context.CompomentClasses.Remove(componentClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
