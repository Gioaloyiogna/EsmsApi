namespace ServiceManagerApi.Dtos.PartsRobbingDto
{
    public class PartsRobbingDto
    {

        public int Id { get; set; }

        public string EquipmentId { get; set; } = null!;

        public string? EquipmentDescription { get; set; }

        public string? Fault { get; set; }

        public string? JobReference { get; set; }

        public string? PartRequired { get; set; }

        public string? RobbedEquipmentId { get; set; }

        public string? ReplacementPlan { get; set; }

        public string? Requestor { get; set; }

        public string? Approver { get; set; }

        public string? Comment { get; set; }

        public string? ActionTaken { get; set; }

        public DateTime? CompletionDate { get; set; }

        public string? Personnel { get; set; }

        public string? Remarks { get; set; }

        public string? ReferenceNo { get; set; }

        public DateTime? EntryDate { get; set; }

        public string? EnteredBy { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? TenantId { get; set; }
        public string? Status { get; set; }
    }
}
