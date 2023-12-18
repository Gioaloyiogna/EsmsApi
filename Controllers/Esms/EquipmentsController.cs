using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.Equipments;

namespace ServiceManagerApi.Controllers.Esms;

[Route("api/[controller]")]
[ApiController]
public class EquipmentsController : BaeApiController<EquipmentPostDto>
{
  private readonly EnpDBContext _context;
    private IWebHostEnvironment webHostEnvironment;

    public EquipmentsController(EnpDBContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        this.webHostEnvironment = webHostEnvironment;
    }

    // GET: api/Equipments/tenant/{tenantId}  with paging 
    [HttpGet("tenant/{tenantId}")]
  public Task<List<Equipment>> GetEquipments(string tenantId,
      [FromQuery] int? pageNumber,
      [FromQuery] int? pageSize)
  {
    var equipments = _context.Equipment
        .Where(equipment => equipment.TenantId == tenantId && equipment.Status == "Active")
        .Include(e => e.Model)
        .Select(e => new Equipment
        {
            Id = e.Id,
            ModelId = e.ModelId,
            EquipmentId = e.EquipmentId,
            Description = e.Description,
            SerialNumber = e.SerialNumber,
            ManufactureDate = e.ManufactureDate,
            PurchaseDate = e.PurchaseDate,
            EndOfLifeDate = e.EndOfLifeDate,
            Facode = e.Facode,
            Note = e.Note,
            WarrantyStartDate = e.WarrantyStartDate,
            WarrantyEndDate = e.WarrantyEndDate,
            UniversalCode = e.UniversalCode,
            MeterType = e.MeterType,
            
            InitialReading = e.InitialReading,
            Adjustment = e.Adjustment,
            HoursEntries = e.HoursEntries
                .Where(entry => entry.TenantId == tenantId && entry.FleetId == e.EquipmentId &&
                                entry.EntrySource == "Normal Reading")
                .OrderByDescending(entry => entry.Date) // Order by the Date property in descending order
                .Take(1) // Take the first (latest) entry
                .ToList(),
            Category =  e.Category,
            CategoryNavigation= _context.Categories.Where(te => te.Id == e.Category).FirstOrDefault(),
            ComissionDate=e.ComissionDate,
            SiteArrivalDate=e.SiteArrivalDate,
            EquipmentPicture=e.EquipmentPicture,
            Model = e.Model != null
                ? new Model
                {
                    ModelId = e.Model.ModelId,
                    ManufacturerId = e.Model.ManufacturerId,
                    ModelClassId = e.Model.ModelClassId,
                    Name = e.Model.Name,
                    Code = e.Model.Code,
                    LubeConfigs = e.Model.LubeConfigs,
                    PictureLink = e.Model.PictureLink,
                    Services = e.Model.Services,
                    Manufacturer = new Manufacturer
                    {
                        ManufacturerId = e.Model.Manufacturer.ManufacturerId,
                        Name = e.Model.Manufacturer.Name
                    },
                    ModelClass = new ModelClass
                    {
                        ModelClassId = e.Model.ModelClass.ModelClassId,
                        Name = e.Model.ModelClass.Name,
                        Code = e.Model.ModelClass.Code
                    },
                    Components= _context.Components.Where(te=>te.ModelId== e.ModelId).ToList(),
                }
                : null
        });

    if (pageNumber.HasValue && pageSize.HasValue)
      equipments = equipments
          .Skip((pageNumber.Value - 1) * pageSize.Value)
          .Take(pageSize.Value);

    return equipments.ToListAsync();
  }
    // GET: api/Equipments/tenant/{tenantId}  with paging 
    [HttpGet("allEquipment/{tenantId}")]
    public Task<List<Equipment>> GetAllEquipments(string tenantId,
      [FromQuery] int? pageNumber,
      [FromQuery] int? pageSize)
    {
        var equipments = _context.Equipment
            .Where(equipment => equipment.TenantId == tenantId)
            .Include(e => e.Model)
            .Select(e => new Equipment
            {
                Id = e.Id,
                ModelId = e.ModelId,
                EquipmentId = e.EquipmentId,
                Description = e.Description,
                SerialNumber = e.SerialNumber,
                ManufactureDate = e.ManufactureDate,
                PurchaseDate = e.PurchaseDate,
                EndOfLifeDate = e.EndOfLifeDate,
                Facode = e.Facode,
                Note = e.Note,
                WarrantyStartDate = e.WarrantyStartDate,
                WarrantyEndDate = e.WarrantyEndDate,
                UniversalCode = e.UniversalCode,
                MeterType = e.MeterType,
                Status=e.Status,
                InitialReading = e.InitialReading,
                Adjustment = e.Adjustment,
                HoursEntries = e.HoursEntries
                    .Where(entry => entry.TenantId == tenantId && entry.FleetId == e.EquipmentId &&
                                    entry.EntrySource == "Normal Reading")
                    .OrderByDescending(entry => entry.Date) // Order by the Date property in descending order
                    .Take(1) // Take the first (latest) entry
                    .ToList(),
                Category = e.Category,
                CategoryNavigation = _context.Categories.Where(te => te.Id == e.Category).FirstOrDefault(),
                ComissionDate = e.ComissionDate,
                SiteArrivalDate = e.SiteArrivalDate,
                EquipmentPicture = e.EquipmentPicture,
                Model = e.Model != null
                    ? new Model
                    {
                        ModelId = e.Model.ModelId,
                        ManufacturerId = e.Model.ManufacturerId,
                        ModelClassId = e.Model.ModelClassId,
                        Name = e.Model.Name,
                        Code = e.Model.Code,
                        LubeConfigs = e.Model.LubeConfigs,
                        PictureLink = e.Model.PictureLink,
                        Services = e.Model.Services,
                        Manufacturer = new Manufacturer
                        {
                            ManufacturerId = e.Model.Manufacturer.ManufacturerId,
                            Name = e.Model.Manufacturer.Name
                        },
                        ModelClass = new ModelClass
                        {
                            ModelClassId = e.Model.ModelClass.ModelClassId,
                            Name = e.Model.ModelClass.Name,
                            Code = e.Model.ModelClass.Code
                        },
                        Components = _context.Components.Where(te => te.ModelId == e.ModelId).ToList(),
                    }
                    : null
            });

        if (pageNumber.HasValue && pageSize.HasValue)
            equipments = equipments
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);

        return equipments.ToListAsync();
    }



    // GET: api/Equipments/5
    [HttpGet("{id}")]
  public async Task<ActionResult<Equipment>> GetEquipment(int id)
  {
    if (_context.Equipment == null) return NotFound();
    var equipment = await _context.Equipment.FindAsync(id);

    if (equipment == null) return NotFound();

    return Ok(equipment);
  }
    


    // PUT: api/Equipments/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
  public async Task<IActionResult> PutEquipment(int id, Equipment equipment)
  {
    if (id != equipment.Id) return BadRequest();

    _context.Entry(equipment).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!EquipmentExists(id))
        return NotFound();
      else
        throw;
    }

    return NoContent();
  }

  //PATCH equipment: api/Equipments/5
  [HttpPatch("{id}")]
  public async Task<IActionResult> PatchEquipment(int id,
      [FromForm] EquipmentUpdateDto data)
  {
    var equipment = await _context.Equipment.FindAsync(id);

    if (equipment == null) return BadRequest();
        if (data.ImageFile != null)
        {
            data.EquipmentPicture = await UploadImage(data.ImageFile);
        }
        else
        {
            data.EquipmentPicture = equipment.EquipmentPicture;
        }
        var equipmentMapper=_mapper.Map<Equipment>(data);
        var patchEquipment = new JsonPatchDocument<Equipment>();
        
        patchEquipment.Replace(e => e.Category, equipmentMapper.Category);
        patchEquipment.Replace(e=>e.SerialNumber, equipmentMapper.SerialNumber);
        patchEquipment.Replace(e => e.Description, equipmentMapper.Description);
        patchEquipment.Replace(e => e.ManufactureDate, equipmentMapper.ManufactureDate);
        patchEquipment.Replace(e => e.ModelId, equipmentMapper.ModelId);
        patchEquipment.Replace(e => e.PurchaseDate, equipmentMapper.PurchaseDate);
        patchEquipment.Replace(e => e.EndOfLifeDate, equipmentMapper.EndOfLifeDate);
        patchEquipment.Replace(e => e.Facode, equipmentMapper.Facode);
        patchEquipment.Replace(e => e.SiteArrivalDate, equipmentMapper.SiteArrivalDate);
        patchEquipment.Replace(e => e.EquipmentPicture, equipmentMapper.EquipmentPicture);
        patchEquipment.ApplyTo(equipment, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        try
        {
            
            await _context.SaveChangesAsync();

            return Ok(equipment);
        }
        catch (Exception ex)
        {
            
            return BadRequest("An error occurred while updating equipment data: " + ex.Message);
        }
    }

  [HttpPatch("updateGeneralInfo/{id}")]
    public async Task<IActionResult> PatchEquipment(int id,
      [FromBody] JsonPatchDocument<Equipment> patchEquipment)
    {
        var equipment = await _context.Equipment.FindAsync(id);

        if (equipment == null) return BadRequest();

        patchEquipment.ApplyTo(equipment, ModelState);

        await _context.SaveChangesAsync();

        return Ok(equipment);
    }
    // POST: api/Equipments
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
  public async Task<ActionResult<Equipment>> PostEquipment([FromForm] EquipmentPostDto equipmentPostDto)
  {
        equipmentPostDto.EquipmentPicture  =await UploadImage(equipmentPostDto.ImageFile);
        var equipment = _mapper.Map<Equipment>(equipmentPostDto);

    if (_context.Equipment == null) return Problem("Entity set 'EnpDBContext.Equipment'  is null.");
    _context.Equipment.Add(equipment);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetEquipment", new { id = equipment.Id }, equipment);
  }
    
    [NonAction]
    public async Task<string> UploadImage(IFormFile? imageFile)
    {

        var mes = "No file was selected";

        if (imageFile != null)
        {
            try
            {
                string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
                var imagePath = Path.Combine(webHostEnvironment.ContentRootPath, "Uploads/Equipment", imageName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                return imageName;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }
        return mes;

    }

    // DELETE: api/Equipments/5
    [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteEquipment(int id)
  {
    if (_context.Equipment == null) return NotFound();
    var equipment = await _context.Equipment.FindAsync(id);
    if (equipment == null) return NotFound();

    _context.Equipment.Remove(equipment);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool EquipmentExists(int id)
  {
    return (_context.Equipment?.Any(e => e.Id == id)).GetValueOrDefault();
  }
   

}