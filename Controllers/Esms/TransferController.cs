using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.FaultEntry;
using ServiceManagerApi.Dtos.Transfer;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.JsonPatch;
using NuGet.Protocol.Plugins;

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
            var transfers= await _context.Transfers.Where(fault => fault.TenantId == tenantId)
                .ToListAsync();

            var transferDtos = transfers.Select(transfer => new Transfer
            {
                Approvals = transfer.Approvals,
                AssetValue = transfer.AssetValue,
                Comment = transfer.Comment,
                ConditionId = transfer.ConditionId,
                Condition = _context.ComponentConditions.FirstOrDefault(te => te.Id == transfer.ConditionId),
                Date = transfer.Date,
                Designation = transfer.Designation,
                DisposalMethod = _context.AssetDisposals.FirstOrDefault(te => te.Id == transfer.DisposalMethodId),
                DisposalMethodId = transfer.DisposalMethodId,
                Disposer = transfer.Disposer,
                DisposalReason = transfer.DisposalReason,
                EquipmentDescription = transfer.EquipmentDescription,
                EquipmentId = transfer.EquipmentId,
                SerialNo = transfer.SerialNo,
                Smu = transfer.Smu,
                Id = transfer.Id,
                ReferenceNo = transfer.ReferenceNo,
                TenantId = transfer.TenantId,
                Status=transfer.Status
            });

            return transferDtos.ToList();
        }
        [HttpPost]
        public async Task<ActionResult<Transfer>> postTransfer(TransferDto transfer)
        {
            String? referenceId = null;
            if (transfer !=null)
            {


                var transfersCount = _context.Transfers.Where(te => te.TenantId == "tarkwa").Count();
                var getCountString = transfersCount.ToString().Length;
                switch (getCountString)
                {
                    case 1:
                        referenceId = "TA" + "00000" + transfersCount;
                        break;
                    case 2:
                        referenceId = "TA" + "000" + transfersCount;
                        break;
                    case 3:
                        referenceId = "TA" + "00" + transfersCount;
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
        [HttpPatch("{id}")]
   
        public async Task<ActionResult> PatchTransfer(int id, [FromBody] JsonPatchDocument<Transfer> assetData)
        {
            try
            {
                var asset = await _context.Transfers.FindAsync(id);

                if (asset == null) return BadRequest("Asset not found");

                
                assetData.ApplyTo(asset, ModelState);

               

                await _context.SaveChangesAsync();

                return Ok(asset);
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
