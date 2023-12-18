namespace ServiceManagerApi.Dtos.GroundEngagingTools;

public class GroundEngagingToolsPostDto
{
  public string EquipmentId { get; set; } = null!;
  public double? PreviousHours { get; set; }
  public double? CurrentHours { get; set; }
  public int? Quantity { get; set; }
  public string? Reason { get; set; }
  public DateTime? Date { get; set; }
  public string? ItemType { get; set; }
  public string TenantId { get; set; } = null!;
    public string? ReferenceNo { get; set; }
    public int? PartId { get; set; }

    public int? PositionId { get; set; }

    public int? SupplierId { get; set; }

    public string? Comment { get; set; }

    public int? TotalCost { get; set; }

    public int? HoursWorked { get; set; }
    public string? PartNo { get; set; }
    public string? CostRef { get; set; }
}