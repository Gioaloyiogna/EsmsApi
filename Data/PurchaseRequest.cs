using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class PurchaseRequest
{
    public int Id { get; set; }

    public string? PurchaseRequisition { get; set; }

    public DateTime? Date { get; set; }

    public string? Details { get; set; }

    public int DepartmentId { get; set; }

    public int SectionId { get; set; }

    public int ReferenceTypeId { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Requestor { get; set; }

    public int GlAccountId { get; set; }

    public string? Description { get; set; }

    public int SupplierId { get; set; }

    public string? TenantId { get; set; }

    public string? EquipmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Equipment? Equipment { get; set; }

    public virtual GlAccount GlAccount { get; set; } = null!;

    public virtual Reference ReferenceType { get; set; } = null!;

    public virtual PucharseSection Section { get; set; } = null!;
}
