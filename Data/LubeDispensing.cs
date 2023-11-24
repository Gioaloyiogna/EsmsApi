using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class LubeDispensing
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public DateTime DispensingDate { get; set; }

    public string EquipmentId { get; set; } = null!;

    public int? Smu { get; set; }

    public int? LubeType { get; set; }

    public int Quantity { get; set; }

    public int? Source { get; set; }

    public int? Reason { get; set; }

    public int? Compartment { get; set; }

    public string? IssueBy { get; set; }

    public string? ReceivedBy { get; set; }

    public string? Comment { get; set; }

    public string TenantId { get; set; } = null!;

    public string? BatchNo { get; set; }

    public virtual Compartment? CompartmentNavigation { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual LubeType? LubeTypeNavigation { get; set; }

    public virtual LubeDispensingReason? ReasonNavigation { get; set; }

    public virtual Source? SourceNavigation { get; set; }
}
