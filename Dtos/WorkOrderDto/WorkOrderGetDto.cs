using ServiceManagerApi.Data;

namespace ServiceManagerApi.Dtos.WorkOrderDto;

public record WorkOrderGetDto()
{
    public int Id { get; set; }

    public string TenantId { get; set; } = null!;

    public string? ReferenceNo { get; set; }

    public DateTime CreatedAt { get; set; }

    public string EquipmentId { get; set; } = null!;

    public string? EquipmentDescription { get; set; }

    public string? WorkOrderType { get; set; }

    public string? WorkOrderCategory { get; set; }

    public string Fault { get; set; } = null!;

    public string? Priority { get; set; }

    public string? Requester { get; set; }

    public string? Receiver { get; set; }

    public string WorkInstruction { get; set; } = null!;

    public string? PermitRequired { get; set; }

    public DateTime ScheduledDate { get; set; }

    public string? WorkDone { get; set; }

    public int? Smu { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public string? Tools { get; set; }

    public string? Parts { get; set; }

    public decimal? Cost { get; set; }

    public int? BacklogId { get; set; }

    public virtual Backlog Backlog { get; set; }
}
