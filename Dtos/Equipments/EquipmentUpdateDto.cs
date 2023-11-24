using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ServiceManagerApi.Dtos.Equipments;

public record EquipmentUpdateDto
{
    [Required] public int Id { get; set; }
    [Required] public int ModelId { get; set; }

    [Required] public string EquipmentId { get; set; } = null!;

    public string? Description { get; set; }

    public string? SerialNumber { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public DateTime? EndOfLifeDate { get; set; }

    public string? Facode { get; set; }

    public string? Note { get; set; }

    public DateTime? WarrantyStartDate { get; set; }
    public DateTime? ComissionDate { get; set; }
    public DateTime? WarrantyEndDate { get; set; }
    public string? EquipmentPicture { get; set; }
    public DateTime? SiteArrivalDate { get; set; }
    public int? Category { get; set; }

    public IFormFile? ImageFile { get; set; }


   
}
