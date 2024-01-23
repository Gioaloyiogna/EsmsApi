namespace ServiceManagerApi.Dtos.PurchaseDto
{
    public class PurchaseDto
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

        public string? EquipmentId { get; set; }
        public string? TenantId { get; set; }

    }
}
