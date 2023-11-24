using ServiceManagerApi.Data;

namespace ServiceManagerApi.Dtos.BacklogDto;

public record BacklogDto
{
    public int Id { get; set; }
    public string EquipmentId { get; set; } = null!;
    public DateTime? Bdate { get; set; }
    public string? Comment { get; set; }
    public string? Status { get; set; }
    public DateTime? Cdate { get; set; }
    public string? TenantId { get; set; }
    public string? Priority { get; set; }
    public string? Source { get; set; }
    public string? DownType { get; set; }
    public string? ReferenceNo { get; set; }
    public int? Smu { get; set; }
    public string? Fault { get; set; }
    public string? Initiator { get; set; }
    public string? AssignedTo { get; set; }
    public int? WorkOrderId { get; set; }
    public string? Remarks { get; set; }

    public virtual WorkOrderDto? WorkOrder { get; set; }
}

public record WorkOrderDto
{
    public string? ReferenceNo { get; set; }
}

public record BacklogPostDto
(
    string EquipmentId,
    DateTime? Bdate,
    int? Smu,
    string Fault,
    string? DownType,
    string? Priority,
    string? Source,
    string? Initiator,
    string? Status,
    string? AssignedTo,
    string? Comment,
    string TenantId
);

public record BacklogPutDto
(
    int Id,
    string EquipmentId,
    DateTime? Bdate,
    string? Item,
    string? Note,
    string? Comment,
    string? ReferenceId,
    string? Status,
    DateTime? Cdate,
    string TenantId
);
