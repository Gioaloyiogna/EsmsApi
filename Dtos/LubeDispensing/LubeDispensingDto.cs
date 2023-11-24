namespace ServiceManagerApi.Dtos.LubeDispensing;

public record LubeDispensingDto
{
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
    public string BatchNo { get; set; }
}
