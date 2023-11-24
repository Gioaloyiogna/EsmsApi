using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class EquipmentComponentSchedule
{
    public int Id { get; set; }

    public string EquipmentId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ModelId { get; set; }

    public int ComponentId { get; set; }

    public int ExpectedLife { get; set; }

    public string? TenantId { get; set; }

    public virtual Component Component { get; set; } = null!;

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual Model Model { get; set; } = null!;
}
