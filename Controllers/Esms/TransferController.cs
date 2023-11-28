using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.FaultEntry;
using ServiceManagerApi.Dtos.Transfer;

namespace ServiceManagerApi.Controllers.Esms
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : BaeApiController<TransferDto>
    {
        private readonly EnpDBContext _context;
        public TransferController(EnpDBContext context) {
            _context = context;
        }
        // GET: api/FaultEntriesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transfer>> GetTransfer(int id)
        {
            var transfers = await _context.Transfers.FindAsync(id);

            if (transfers == null) return NotFound();

            return transfers;
        }
        // GET: api/FaultEntriesApi
        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<Transfer>>> GetTransfers(string tenantId)
        {
            return await _context.Transfers.Where(fault => fault.TenantId == tenantId)
                .ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Transfer>> postTransfer(TransferDto transfer)
        {
            String? referenceId = null;
            if (transfer !=null)
            {


                var transfersCount = _context.Transfers.Where(te => te.TenantId == "tarkwa").Count();
                switch (transfersCount)
                {
                    case 1:
                        referenceId = "TA" + "00000" + transfersCount;
                        break;
                    case 2:
                        referenceId = "TA" + "000" + transfersCount;
                        break;
                    case 3:
                        referenceId = "TD" + "00" + transfersCount;
                        break;
                    case 4:
                        referenceId = "TA" + "0" + transfersCount;
                        break;
                    default:
                        referenceId = "TA" + transfersCount;
                        break;


                }
                transfer.ReferenceNo = referenceId;
                var transferMapping = _mapper.Map<Transfer>(transfer);
                _context.Transfers.Add(transferMapping);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (TransferExists(transfer.Id))
                        return Conflict();
                    else
                        throw;
                }

                return CreatedAtAction("GetTransfer", new { id = transfer.Id }, transfer);

            }
            return Problem("Transfer data object is null");

           
            


        }
        private bool TransferExists(int id)
        {
            return _context.Transfers.Any(e => e.Id == id);
        }


    }
}
