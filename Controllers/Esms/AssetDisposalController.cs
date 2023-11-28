using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;

namespace ServiceManagerApi.Controllers.Esms
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetDisposalController : ControllerBase
    {
        private readonly EnpDBContext _context;

        public AssetDisposalController(EnpDBContext context)
        {
            _context = context;
        }
        [HttpGet("tenant/{tenantId}")]
        public Task<List<AssetDisposal>> GetDisposalMethods(string tenantId)
        {
            var assets= _context.AssetDisposals.Where(leav => leav.TenantId == tenantId).ToListAsync();

            return assets;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDisposal>> getDisposalMethod(string id)
        {
            if (_context.AssetDisposals == null)
            {
                return NotFound();
            }
            var  disposalMethod = await _context.AssetDisposals.FindAsync(id);

            if (disposalMethod == null)
            {
                return NotFound();
            }

            return disposalMethod;
        }

        [HttpPost]
        public async Task<ActionResult<AssetDisposal>> postDisposalMethod(AssetDisposal assetDisposal)
        {
            if(assetDisposal == null)
            {
                return Problem("Entity set 'EnpDBContext.AssetDisposal'  is null.");
            }
            _context.AssetDisposals.Add(assetDisposal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getDisposalMethod", new { id = assetDisposal.Id },assetDisposal);

        }
        // PUT: api/AssetDisposal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssetMethod(int id, AssetDisposal asset)
        {
            if (id != asset.Id)
            {
                return BadRequest();
            }

            _context.Entry(asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
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
        private bool AssetExists(int id)
        {
            return (_context.AssetDisposals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetMethod(int id)
        {
            if (_context.AssetDisposals == null)
            {
                return NotFound();
            }
            var asset = await _context.AssetDisposals.FindAsync(id);
            if ( asset == null)
            {
                return NotFound();
            }

            _context.AssetDisposals.Remove(asset);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
