namespace ServiceManagerApi.Dtos.Transfer
{
    public class TransferDto
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public string EquipmentId { get; set; } = null!;

        public string EquipmentDescription { get; set; } = null!;

        public string? ReferenceNo { get; set; }

        public string SerialNo { get; set; } = null!;

        public string? Smu { get; set; }

        public int ConditionId { get; set; }

        public string DisposalReason { get; set; } = null!;

        public int DisposalMethodId { get; set; }

        public int? AssetValue { get; set; }

        public string Disposer { get; set; } = null!;

        public string Designation { get; set; } = null!;

        public string Approvals { get; set; } = null!;

        public string? ActionTaken { get; set; }

        public DateTime? CompletionDate { get; set; }

        public string? Personnel { get; set; }

        public string? WayBill { get; set; }

        public string? AdditionalComment { get; set; }

        public int? Status { get; set; }
        public string? Comment { get; set; }
        public string TenantId { get; set; } = null!;


    }
}
