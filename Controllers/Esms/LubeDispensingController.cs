using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.LubeDispensing;

namespace ServiceManagerApi.Controllers.Esms;

[Route("api/[controller]")]
[ApiController]
public class LubeDispensingController : BaeApiController<LubeDispensingController>
{
    private readonly EnpDBContext _context;

    public LubeDispensingController(EnpDBContext context)
    {
        _context = context;
    }

    // GET: api/LubeDispensing/tenant/{tenantId}?batchNo={batchNo}
    [HttpGet("tenant/{tenantId}")]
    public async Task<ActionResult<IEnumerable<LubeDispensing>>> GetLubeDispensings(string tenantId,
        [FromQuery] string? batchNo
    )
    {
        var userId = batchNo?.Split('-')[0];
        batchNo = batchNo == null ? null : $"{userId}-{DateTime.UtcNow:yyyyMMdd}";

        if (_context.LubeDispensings == null) return NotFound();
        return await _context.LubeDispensings
            .Where(lubeDispensing =>
                lubeDispensing.TenantId == tenantId && (batchNo == null || lubeDispensing.BatchNo == batchNo))
            .Select(lubeDispensing => new LubeDispensing
            {
                Id = lubeDispensing.Id,
                DispensingDate = lubeDispensing.DispensingDate,
                EquipmentId = lubeDispensing.EquipmentId,
                ReferenceNo = lubeDispensing.ReferenceNo,
                Smu = lubeDispensing.Smu,
                LubeType = lubeDispensing.LubeType,
                Quantity = lubeDispensing.Quantity,
                Source = lubeDispensing.Source,
                Reason = lubeDispensing.Reason,
                Compartment = lubeDispensing.Compartment,
                IssueBy = lubeDispensing.IssueBy,
                ReceivedBy = lubeDispensing.ReceivedBy,
                Comment = lubeDispensing.Comment,
                TenantId = lubeDispensing.TenantId,
                BatchNo = lubeDispensing.BatchNo,
                Equipment = new Equipment
                {
                    Description = lubeDispensing.Equipment.Description,
                    EquipmentId = lubeDispensing.EquipmentId
                },
                LubeTypeNavigation = new LubeType
                {
                    Name = lubeDispensing.LubeTypeNavigation.Name,
                    LubeDescription = lubeDispensing.LubeTypeNavigation.LubeDescription
                },
                ReasonNavigation = new LubeDispensingReason
                {
                    Name = lubeDispensing.ReasonNavigation.Name
                },
                CompartmentNavigation = new Compartment
                {
                    Name = lubeDispensing.CompartmentNavigation.Name
                },
                SourceNavigation = new Source
                {
                    Name = lubeDispensing.SourceNavigation.Name
                }
            })
            .ToListAsync();
    }

    // GET: api/LubeDispensing/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LubeDispensing>> GetLubeDispensing(int id)
    {
        if (_context.LubeDispensings == null) return NotFound();
        var lubeDispensing = await _context.LubeDispensings.FindAsync(id);

        if (lubeDispensing == null) return NotFound();

        return lubeDispensing;
    }


    // PUT: api/LubeDispensing/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLubeDispensing(int id, LubeDispensingPutDto lubeDispensingPutDto)
    {
        var lubeDispensing = _mapper.Map<LubeDispensing>(lubeDispensingPutDto);
        if (id != lubeDispensing.Id) return BadRequest();

        _context.Entry(lubeDispensing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LubeDispensingExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/LubeDispensing
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(typeof(LubeDispensing), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostLubeDispensing(LubeDispensingDto lubeDispensingDto)
    {
        var lubeDispensing = _mapper.Map<LubeDispensing>(lubeDispensingDto);

        lubeDispensing.BatchNo = $"{lubeDispensing.BatchNo}-{DateTime.UtcNow:yyyyMMdd}";
        _context.LubeDispensings.Add(lubeDispensing);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (LubeDispensingExists(lubeDispensing.Id))
                return Conflict();
            return Problem();
        }

        return CreatedAtAction(nameof(GetLubeDispensing), new { id = lubeDispensing.Id }, lubeDispensing);
    }

    // DELETE: api/LubeDispensing/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLubeDispensing(int id)
    {
        if (_context.LubeDispensings == null) return NotFound();
        var lubeDispensing = await _context.LubeDispensings.FindAsync(id);
        if (lubeDispensing == null) return NotFound();

        _context.LubeDispensings.Remove(lubeDispensing);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LubeDispensingExists(int id)
    {
        return (_context.LubeDispensings?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
