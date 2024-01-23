using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.GroundEngagingTools;
using ServiceManagerApi.Dtos.PurchaseDto;
using SQLitePCL;

namespace ServiceManagerApi.Controllers.Esms
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseRequisitionController : BaeApiController<PurchaseDto>
    {
        private readonly EnpDBContext _context;
        public PurchaseRequisitionController(EnpDBContext context)
        {
            _context = context;
        }
        //Getting all purchases by TenantId

        [HttpGet("tenant/{tenantId}")]
        public Task<List<PurchaseRequest>> GetPurchaseByTenantId(string tenantId)
        {
            var purchases = _context.PurchaseRequests.Where(te => te.TenantId == tenantId).Select(te => new PurchaseRequest
            {
                Id = te.Id,
                TenantId = te.TenantId,
                PurchaseRequisition = te.PurchaseRequisition,
                Date = te.Date,
                ReferenceNo = te.ReferenceNo,
                Description = te.Description,
                Details = te.Details,
                GlAccount = _context.GlAccounts.Where(e => e.Id == te.GlAccountId).FirstOrDefault() ?? new GlAccount { },
                Department = _context.Departments.Where(e => e.Id == te.DepartmentId).FirstOrDefault() ?? new Department { },
                Section = _context.PucharseSections.Where(e => e.Id == te.SectionId).FirstOrDefault() ?? new PucharseSection { },
                ReferenceType = _context.References.Where(e => e.Id == te.ReferenceTypeId).FirstOrDefault() ?? new Reference { },
                Requestor = te.Requestor,
                SupplierId = te.SupplierId,
                EquipmentId=te.EquipmentId,

            }).ToListAsync();

            return purchases;
        }
        // getting siuppliers by Id
        // GET: api/GroundEngagingTools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseRequest>> GetPurchaseRequest(int id)
        {
            if (_context.PurchaseRequests == null)
            {
                return NotFound();
            }
            var purchases = await _context.PurchaseRequests.Where(tool => tool.Id == id).Select(te => new PurchaseRequest
            {
                Id = te.Id,
                TenantId = te.TenantId,
                PurchaseRequisition = te.PurchaseRequisition,
                Date = te.Date,
                ReferenceNo = te.ReferenceNo,
                Description = te.Description,
                Details = te.Details,
                GlAccount = _context.GlAccounts.Where(e => e.Id == te.GlAccountId).FirstOrDefault() ?? new GlAccount { },
                Department = _context.Departments.Where(e => e.Id == te.DepartmentId).FirstOrDefault() ?? new Department { },
                Section = _context.PucharseSections.Where(e => e.Id == te.SectionId).FirstOrDefault() ?? new PucharseSection { },
                ReferenceType = _context.References.Where(e => e.Id == te.ReferenceTypeId).FirstOrDefault() ?? new Reference { },
                Requestor = te.Requestor,
                SupplierId = te.SupplierId,
                EquipmentId = te.EquipmentId,

            }).ToListAsync();

            if (purchases == null)
            {
                return NotFound();
            }

            return Ok(purchases);
        }

        // posting data
        [HttpPost]
        public async Task<ActionResult<PurchaseRequest>> PostPurchase(PurchaseDto purchaseDto)
        {
            if (_context.PurchaseRequests == null)
            {
                return Problem("Entity set 'EnpDBContext..PurchaseRequests  is null.");
            }

            String? referenceId = null;
            


                var getCount = _context.PurchaseRequests.Where(te => te.TenantId == "tarkwa").Count();
                var getCountString = getCount.ToString().Length;

                switch (getCountString)
                {
                    case 1:
                        referenceId = "PR" + "00000" + getCount;
                        break;
                    case 2:
                        referenceId = "PR" + "000" + getCount;
                        break;
                    case 3:
                        referenceId = "PR" + "00" + getCount;
                        break;
                    case 4:
                        referenceId = "PR" + "0" + getCount;
                        break;
                    default:
                        referenceId = "PR" + getCount;
                        break;
                }


            
            purchaseDto.PurchaseRequisition = referenceId;

            PurchaseRequest purchase =_mapper.Map<PurchaseRequest>(purchaseDto);
            _context.PurchaseRequests.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseRequest", new { id = purchaseDto.Id }, purchaseDto);
        }


    }


}
