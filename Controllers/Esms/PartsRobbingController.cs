using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.FaultEntry;
using ServiceManagerApi.Dtos.PartsRobbingDto;

namespace ServiceManagerApi.Controllers.Esms
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsRobbingController : BaeApiController<PartsRobbingDto>
    {
        private readonly EnpDBContext _context;

        public PartsRobbingController(EnpDBContext context) {
            _context = context;

        }
        [HttpGet("tenant/{tenant}")]
        public async Task<ActionResult<List<PartsRobbing>>> GetPartsRobbingByTenant(string tenant )
        {
            if(tenant== null)
            {
                return StatusCode(500, "tenant is null");
            }
            try
            {
                var partsRobbing = await _context.PartsRobbings.Where(te=>te.TenantId==tenant).ToListAsync();
                return Ok(partsRobbing);
            }
            catch (DbUpdateException ex)
            {
      
                return StatusCode(500, $"Database Operation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        //getting parts robbing by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<PartsRobbing>> GetPartsRobbing(int id)
        {
            
            try
            {
                var partsRobbing = await _context.PartsRobbings.FindAsync(id);
                return Ok(partsRobbing);
            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, $"Database Operation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPost]
        //posting parts robbing
        public async Task<ActionResult<PartsRobbing>> PostPartsRobbing(PartsRobbingDto partsRobbingDto)
        {
             if(partsRobbingDto == null)
            {
                return StatusCode(500, "Entity is null");

            }
            try
            {
                String? referenceId = null;
                if (partsRobbingDto.TenantId == "tarkwa")
                {


                    var partsCount = _context.PartsRobbings.Where(te => te.TenantId == "tarkwa").Count();
                    var partsCountTostring = partsCount.ToString().Length;
                    switch (partsCountTostring)
                    {
                        case 1:
                            referenceId = "TD" + "00000" + partsCount;
                            break;
                        case 2:
                            referenceId = "TD" + "000" + partsCount;
                            break;
                        case 3:
                            referenceId = "TD" + "00" + partsCount;
                            break;
                        case 4:
                            referenceId = "TD" + "0" + partsCount;
                            break;
                        default:
                            referenceId = "TD" + partsCount;
                            break;
                    }


                }
               partsRobbingDto.ReferenceNo= referenceId;
                var mappingData=_mapper.Map<PartsRobbing>(partsRobbingDto);

                _context.PartsRobbings.Add(mappingData);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPartSRobbing", new { id = partsRobbingDto.Id}, partsRobbingDto);
            }
            catch(DbUpdateException) {
                if (PartsRobbingExists(partsRobbingDto.Id))
                    return Conflict();
                else
                    throw;
            }
            catch (Exception ex) {
                return StatusCode(500, $"Error occured while posting data{ex.Message}");
            }

        }
        private bool PartsRobbingExists(int id)
        {
            return _context.PartsRobbings.Any(e =>e.Id == id);
        }
        //patch partsRobbing
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchParts(int id, [FromBody] JsonPatchDocument<PartsRobbing> patchPartsRobbing)
        {
            var parts = await _context.PartsRobbings.FindAsync(id);

            if (parts == null) return BadRequest();

            patchPartsRobbing.ApplyTo(parts, ModelState);

            await _context.SaveChangesAsync();

            return Ok(parts);
        }
    }
}
