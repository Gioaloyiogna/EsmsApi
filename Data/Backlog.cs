using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class Backlog
{
    public int Id { get; set; }

    public string EquipmentId { get; set; } = null!;

    public DateTime? Bdate { get; set; }

    public string? Comment { get; set; }

    public string? Status { get; set; }

    public DateTime? Cdate { get; set; }

    public string TenantId { get; set; } = null!;

    public string? Priority { get; set; }

    public string? Source { get; set; }

    public string? DownType { get; set; }

    public int? Smu { get; set; }

    public string? Fault { get; set; }

    public string? Initiator { get; set; }

    public string? AssignedTo { get; set; }

    public int? WorkOrderId { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Remarks { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual WorkOrder? WorkOrder { get; set; }

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
