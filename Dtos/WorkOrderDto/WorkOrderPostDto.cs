namespace ServiceManagerApi.Dtos.WorkOrderDto;

public record WorkOrderPostDto()
{
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
}
