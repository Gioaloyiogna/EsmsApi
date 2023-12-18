using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.Component;
using ServiceManagerApi.Dtos.Equipments;

namespace ServiceManagerApi.Controllers.Esms
{
    public class EquipmentComponentController : BaeApiController<EquipmentComponentScheduleDto>
    {
        private readonly EnpDBContext _context;
        private IWebHostEnvironment webHostEnvironment;

        public EquipmentComponentController(EnpDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        // GET: api/Equipments/tenant/{tenantId}  with paging 
        [HttpGet("tenant/{tenantId}")]
        public Task<List<EquipmentComponentSchedule>> GetEquipmentComponent(string tenantId)
        
        {
            var components = _context.  EquipmentComponentSchedules       
                   .Where(component => component.TenantId == tenantId)
                .Select(e => new EquipmentComponentSchedule
                {
                     Id=e.Id,
                    TenantId=e.TenantId,
                    ModelId=e.ModelId,
                    ComponentId=e.ComponentId,
                    EquipmentId=e.EquipmentId,
                    Description=e.Description,
                    ExpectedLife=e.ExpectedLife,
                    Model = _context.Models.Where(te => te.ModelId == e.ModelId).FirstOrDefault() ?? new Model { },
                    Component=_context.Components.Where(te=> te.Id== e.ComponentId).FirstOrDefault()?? new Component { }
                 });

            

            return components.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentComponentSchedule>> GetEquipmentComponent(int id)
        {
            if (_context.EquipmentComponentSchedules == null) return NotFound();
            var equipment = await _context.EquipmentComponentSchedules.FindAsync(id);

            if (equipment == null) return NotFound();

            return Ok(equipment);
        }
        // adding components to equipment
        [HttpPost]
        public async Task<ActionResult<EquipmentComponentSchedule>> PostComponentToEquipment(EquipmentComponentScheduleDto   equipmentComponent)
        {

            var equipment =_mapper.Map<EquipmentComponentSchedule>(equipmentComponent);

            if (_context.EquipmentComponentSchedules == null) return Problem("Entity set 'EnpDBContext.EquipmentComponentSchedules'  is null.");
            _context.EquipmentComponentSchedules.Add(equipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipmentComponent", new { id = equipment.Id }, equipment);
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchEquipmentComponent(int id, [FromBody] JsonPatchDocument<EquipmentComponentSchedule> patchComponent)
        {
            var component= await _context.EquipmentComponentSchedules.FindAsync(id);

            if (component== null) return BadRequest();

            patchComponent.ApplyTo(component, ModelState);

            await _context.SaveChangesAsync();

            return Ok(component);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var componentDelete = await _context.EquipmentComponentSchedules.FindAsync(id);
            if (componentDelete == null) return NotFound();
            _context.EquipmentComponentSchedules.Remove(componentDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

