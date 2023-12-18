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

        [HttpDelete("department/tenant/{Id}")]
        public async Task<ActionResult> DeleteDepartmentsByTenant(string Id)
        {
            try
            {
                if (Id == null)
                {
                    return StatusCode(500, "Tenant Id is null");
                }

                var departmentsToDelete = _context.Departments.Find(Id);

                if (departmentsToDelete == null)
                {
                    return StatusCode(500, "No departments found for the given Id");
                }

                _context.Departments.RemoveRange(departmentsToDelete);
                await _context.SaveChangesAsync();

                return Ok("Departments deleted successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Error occurred while deleting departments");
            }
        }


        //fetching all sections by tenentId
        [HttpGet("section/tenant/{tenantId}")]
        public async Task<ActionResult<PucharseSection>> getSectionByTenant(string tenantId)
        {
            if (tenantId == null)
            {
                return StatusCode(500, "Tenant Id is null");
            }
            try
            {
                var allSections = await _context.PucharseSections.Where((te => te.TenantId == tenantId)).ToListAsync();
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

        [HttpDelete("section/tenant/{Id}")]
        public async Task<ActionResult> DeleteSectionsByTenant(string Id)
        {
            try
            {
                if (Id == null)
                {
                    return StatusCode(500, "Tenant Id is null");
                }

                var sectionsToDelete = _context.PucharseSections.Find(Id);

                if (sectionsToDelete == null)
                {
                    return StatusCode(500, "No sections found for the given tenantId");
                }

                _context.PucharseSections.RemoveRange(sectionsToDelete);
                await _context.SaveChangesAsync();

                return Ok("Sections deleted successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Error occurred while deleting sections");
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

        [HttpDelete("reference/tenant/{Id}")]
        public async Task<ActionResult> DeleteReferencesByTenant(string Id)
        {
            try
            {
                if (Id == null)
                {
                    return StatusCode(500, "Id is null");
                }

                var referencesToDelete = _context.References.Find(Id);

                if (referencesToDelete == null)
                {
                    return StatusCode(500, "No references found for the given Id");
                }

                _context.References.RemoveRange(referencesToDelete);
                await _context.SaveChangesAsync();

                return Ok("References deleted successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Error occurred while deleting references");
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

        [HttpPut("department/{id}")]
        public async Task<ActionResult<Department>> PutDepartment(int id, Department updatedDepartment)
        {
            if (id != updatedDepartment.Id)
            {
                return BadRequest("Mismatched department ID in the request body and URL");
            }

            if (!_context.Departments.Any(d => d.Id == id))
            {
                return NotFound($"Department with ID {id} not found");
            }

            if (updatedDepartment == null)
            {
                return StatusCode(500, "Data to be updated is null");
            }

            try
            {
                _context.Entry(updatedDepartment).State = EntityState.Modified;
                await _context.SaveChangesAsync(); // Use async version of SaveChanges
                return NoContent(); // 204 No Content is returned for a successful update
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, $"Concurrency error occurred: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while updating record: {ex.Message}");
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
