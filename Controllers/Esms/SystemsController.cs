using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using System.Net;

namespace ServiceManagerApi.Controllers.Esms
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemsController : ControllerBase
    {
        EnpDBContext _context;
        public SystemsController(EnpDBContext context)
        {
            _context = context;
        }
        // GET: api/systems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemType>> GetSystems(int id)
        {
            if (_context.SystemTypes == null)
            {
                return NotFound();
            }
            var system= await _context.SystemTypes.FindAsync(id);

            if (system == null)
            {
                return NotFound();
            }

            return system;
        }
        // Getting all systems
        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<SystemType>> getSystemsByTenant(string tenantId)
        {
            if (tenantId == null)
            {
                return BadRequest("TenantId cannot be null ");
            }
            try
            {
                
                var systems = await _context.SystemTypes.Where(te => te.TenantId == tenantId).ToListAsync();

                if (systems == null || !systems.Any())
                {
                    return Ok(systems); 
                }

                return Ok(systems);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        // posting systems
        [HttpPost]
        public async Task<ActionResult<SystemType>> PostSystems(SystemType systemType)
        {
               if(systemType == null)
            {
                return Problem("system cannot be null");
            }

            try
            {
                _context.SystemTypes.Add(systemType);
                await _context.SaveChangesAsync();
                return Ok(systemType);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Adding  system type failed");
            }
        }
    }
}
