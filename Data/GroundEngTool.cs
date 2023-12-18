using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class GroundEngTool
{
    public string EquipmentId { get; set; } = null!;

    public int Id { get; set; }

    public double? PreviousHours { get; set; }

    public double? CurrentHours { get; set; }

    public int? Quantity { get; set; }

    public string? Reason { get; set; }

    public DateTime? Date { get; set; }

    public string? TenantId { get; set; }

    public string? ItemType { get; set; }

    public int? PartId { get; set; }

    public int? PositionId { get; set; }

    public int? SupplierId { get; set; }

    public string? Comment { get; set; }

    public int? TotalCost { get; set; }

    public int? HoursWorked { get; set; }

    public string? CostRef { get; set; }

    public string? PartNo { get; set; }

    public string? ReferenceNo { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual PartId? Part { get; set; }

    public virtual Position? Position { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
