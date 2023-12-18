using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using ServiceManagerApi.Data;
using SQLitePCL;

namespace ServiceManagerApi.Controllers.Esms
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly EnpDBContext  _context;
        public PurchaseController(EnpDBContext context)
        {
            _context = context;
        }

        //fetching all departments by tenentId
        [HttpGet("department/tenant/{tenantId}")]
        public async Task<ActionResult<Department>> getDepartmentByTenant(string tenantId)
        {
            if (tenantId==null)
            {
                return StatusCode(500,"Tenant Id is null");
            }
            try
            {
                var allDepartment = await _context.Departments.Where((te => te.TenanId == tenantId)).ToListAsync();
                if (allDepartment == null)
                {
                    return StatusCode(500, "Error occurred while fetching records");
                }
                return Ok(allDepartment);

            }
            catch(Exception ex) { }
            {
               return BadRequest();
            }
        }
        //fetching all sections by tenentId
        [HttpGet("section/tenant/{tenantId}")]
        public async Task<ActionResult<Department>> getSectionByTenant(string tenantId)
        {
            if (tenantId == null)
            {
                return StatusCode(500, "Tenant Id is null");
            }
            try
            {
                var allSections = await _context.Sections.Where((te => te.TenantId == tenantId)).ToListAsync();
                if (allSections == null)
                {
                    return StatusCode(500, "Error occurred while fetching records");
                }
                return Ok(allSections);

            }
            catch (Exception ex) { }
            {
                return BadRequest();
            }
        }
        //fetching all  by tenentId
        [HttpGet("reference/tenant/{tenantId}")]
        public async Task<ActionResult<Department>> getReferenceByTenant(string tenantId)
        {
            if (tenantId == null)
            {
                return StatusCode(500, "Tenant Id is null");
            }
            try
            {
                var allReferences = await _context.References.Where((te => te.TenantId== tenantId)).ToListAsync();
                if (allReferences == null)
                {
                    return StatusCode(500, "Error occurred while fetching records");
                }
                return Ok(allReferences);

            }
            catch (Exception ex) { }
            {
                return BadRequest();
            }
        }

        //fetching all departments by Id
        [HttpGet("department/{Id}")]
        public async Task<ActionResult<Department>> getDepartmentById(int Id)
        {
            
            try
            {
                var allDepartment = await _context.Departments.Where((te => te.Id == Id)).ToListAsync();
                if (allDepartment == null)
                {
                    return StatusCode(500, "Error occurred while fetching records");
                }
                return Ok(allDepartment);

            }
            catch (Exception ex) { }
            {
                return BadRequest();
            }
        }
        //fetching all sections by Id
        [HttpGet("section/{Id}")]
        public async Task<ActionResult<PucharseSection>> getSectionById(int Id)
        {
           
            try
            {
                var allSections = await _context.PucharseSections.Where((te => te.Id == Id)).ToListAsync();
                if (allSections == null)
                {
                    return StatusCode(500, "Error occurred while fetching records");
                }
                return Ok(allSections);

            }
            catch (Exception ex) { }
            {
                return BadRequest();
            }
        }
        //fetching all  by Id
        [HttpGet("reference/{Id}")]
        public async Task<ActionResult<Department>> getReferenceById(int Id)
        {
            
            try
            {
                var allReferences = await _context.References.Where((te => te.Id == Id)).ToListAsync();
                if (allReferences == null)
                {
                    return StatusCode(500, "Error occurred while fetching records");
                }
                return Ok(allReferences);

            }
            catch (Exception ex) { }
            {
                return BadRequest();
            }
        }

        // posting Departments
        [HttpPost("department")]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            if (department == null)
            {
                return StatusCode(500, "Data to be posted is null");
            }

            try
            {
                _context.Add(department);
                await _context.SaveChangesAsync(); // Use async version of SaveChanges
                return CreatedAtAction("GetDepartmentById", new { id = department.Id }, department);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while posting record: {ex.Message}");
            }
        }
        // posting Sections
        [HttpPost("section")]
        public async Task<ActionResult<PucharseSection>> PostSection(Section section)
        {
            if (section == null)
            {
                return StatusCode(500, "Data to be posted is null");
            }

            try
            {
                _context.Add(section);
                await _context.SaveChangesAsync(); // Use async version of SaveChanges
                return CreatedAtAction("GetSectionById", new { id = section.Id }, section);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while posting record: {ex.Message}");
            }
        }
        //posting 
        [HttpPost("reference")]
        public async Task<ActionResult<Reference>> PostSection(Reference reference)
        {
            if (reference == null)
            {
                return StatusCode(500, "Data to be posted is null");
            }

            try
            {
                _context.Add(reference);
                await _context.SaveChangesAsync(); // Use async version of SaveChanges
                return CreatedAtAction("GetSectionById", new { id = reference.Id }, reference);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while posting record: {ex.Message}");
            }
        }


    }
}
