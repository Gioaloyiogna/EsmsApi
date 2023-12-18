using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.FaultEntry;
using ServiceManagerApi.Dtos.GroundEngagingTools;

namespace ServiceManagerApi.Controllers.Esms
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroundEngagingToolsController : BaeApiController<GroundEngagingToolsController>
    {
        private readonly EnpDBContext _context;

        public GroundEngagingToolsController(EnpDBContext context)
        {
            _context = context;
        }

        [HttpGet("tenant/{tenantId}")]
        public Task<List<GroundEngTool>> GetGroundEngTools(string tenantId)
        {
            var groundEngTools = _context.GroundEngTools.Where(leav => leav.TenantId == tenantId).ToListAsync();

            return groundEngTools;
        }
        [HttpGet("tenant/partid/{tenantId}")]
        public Task<List<PartId>> GetPartId(string tenantId)
        {
            var partId= _context.PartIds.Where(leav => leav.Tenant == tenantId).ToListAsync();

            return partId;
        }
        [HttpGet("tenant/position/{tenantId}")]
        public Task<List<Position>> GetPosition(string tenantId)
        {
            var positions = _context.Positions.Where(leav => leav.TenantId == tenantId).ToListAsync();

            return positions;
        }
        [HttpGet("tenant/reasons/{tenantId}")]
        public Task<List<Reason>> GetReasons(string tenantId)
        {
            var reasons = _context.Reasons.Where(leav => leav.TenantId == tenantId).ToListAsync();

            return reasons;
        }
        [HttpGet("tenant/suppliers/{tenantId}")]
        public Task<List<Supplier>> GetSuppliers(string tenantId)
        {
            var suppliers = _context.Suppliers.Where(leav => leav.TenantId == tenantId).ToListAsync();

            return suppliers;
        }
        // GET: api/GroundEngagingTools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroundEngTool>> GetGroundEngTool(int id)
        {
          if (_context.GroundEngTools == null)
          {
              return NotFound();
          }
          var groundEngTool = await _context.GroundEngTools.Include(tool => tool.Equipment).FirstOrDefaultAsync(tool => tool.Id == id);

            if (groundEngTool == null)
            {
                return NotFound();
            }

            return groundEngTool;
        }
        // GET: api/partId/5
        [HttpGet("partId/{id}")]
        public async Task<ActionResult<PartId>> GetPartId(int id)
        {
            if (_context.PartIds == null)
            {
                return NotFound();
            }
            var partId = await _context.PartIds.FindAsync(id);

            if (partId == null)
            {
                return NotFound();
            }

            return partId;
        }
        // GET: api/partId/5
        [HttpGet("position/{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var position = await _context.Positions.FindAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }
        [HttpGet("reason/{id}")]
        public async Task<ActionResult<Reason>> GetReason(int id)
        {
            if (_context.Reasons == null)
            {
                return NotFound();
            }
            var reason = await _context.Reasons.FindAsync(id);

            if (reason == null)
            {
                return NotFound();
            }

            return reason;
        }
        [HttpGet("supplier/{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            if (_context.Suppliers == null)
            {
                return NotFound();
            }
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }


        // POST: api/PartId
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("partid")]
        public async Task<ActionResult<PartId>> PostPartId(PartId partId)
        {
            if (_context.PartIds == null)
            {
                return Problem("Entity set 'EnpDBContext.PartId'  is null.");
            }
            _context.PartIds.Add(partId);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartId", new { id = partId.Id }, partId);
        }
        // POST: api/Position
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("position")]
        public async Task<ActionResult<Position>> PostPosition(Position position)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'EnpDBContext.PartId'  is null.");
            }
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { id = position.Id }, position);
        }
        // POST: api/Position
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("reason")]
        public async Task<ActionResult<Reason>> PostReason(Reason reason)
        {
            if (_context.Reasons== null)
            {
                return Problem("Entity set 'EnpDBContext.Reasons'  is null.");
            }
            _context.Reasons.Add(reason);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReason", new { id = reason.Id }, reason);
        }
        [HttpPost("supplier")]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
            if (_context.Suppliers == null)
            {
                return Problem("Entity set 'EnpDBContext.suppliers'  is null.");
            }
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplier", new { id = supplier.Id }, supplier);
        }
        // PUT: api/GroundEngagingTools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroundEngTool(int id, GroundEngagingToolsPutDto groundEngagingToolsPutDto)
        {
            
            GroundEngTool groundEngTool = _mapper.Map<GroundEngTool>(groundEngagingToolsPutDto);
            if (id != groundEngTool.Id)
            {
                return BadRequest();
            }
            
            _context.Entry(groundEngTool).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroundEngToolExists(id))
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

        // POST: api/GroundEngagingTools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroundEngTool>> PostGroundEngTool(GroundEngagingToolsPostDto groundEngagingToolsPostDto)
        {
          if (_context.GroundEngTools == null)
          {
              return Problem("Entity set 'EnpDBContext.GroundEngTools'  is null.");
          }
          
            String? referenceId = null;
            if (groundEngagingToolsPostDto.TenantId == "tarkwa")
            {


                var getCount = _context.GroundEngTools.Where(te => te.TenantId == "tarkwa").Count();
                var getCountString = getCount.ToString().Length;

                switch (getCountString)
                {
                    case 1:
                        referenceId = "TG" + "00000" + getCount;
                        break;
                    case 2:
                        referenceId = "TG" + "000" + getCount;
                        break;
                    case 3:
                        referenceId = "TG" + "00" + getCount;
                        break;
                    case 4:
                        referenceId = "TG" + "0" + getCount;
                        break;
                    default:
                        referenceId = "TG" + getCount;
                        break;
                }


            }
            groundEngagingToolsPostDto.ReferenceNo= referenceId;

            GroundEngTool groundEngTool = _mapper.Map<GroundEngTool>(groundEngagingToolsPostDto);
            _context.GroundEngTools.Add(groundEngTool);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroundEngTool", new { id = groundEngTool.Id }, groundEngTool);
        }

        // DELETE: api/GroundEngagingTools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroundEngTool(int id)
        {
            if (_context.GroundEngTools == null)
            {
                return NotFound();
            }
            var groundEngTool = await _context.GroundEngTools.FindAsync(id);
            if (groundEngTool == null)
            {
                return NotFound();
            }

            _context.GroundEngTools.Remove(groundEngTool);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroundEngToolExists(int id)
        {
            return (_context.GroundEngTools?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> patchGet(int id, [FromBody] JsonPatchDocument<GroundEngTool> getData)
        {
            try
            {
                var get = await _context.GroundEngTools.FindAsync(id);

                if (get == null) return BadRequest("GET not found");


                getData.ApplyTo(get, ModelState);



                await _context.SaveChangesAsync();

                return Ok(get);
            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, $"An error occurred while saving changes to the entity. See the inner exception for details: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
