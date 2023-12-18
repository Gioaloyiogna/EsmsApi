namespace ServiceManagerApi.Dtos.Component
{
    public class TrackerDto
    {
        public int Id { get; set; }

        public string? ComponentId { get; set; }

        public string ComponentName { get; set; } = null!;

        public string? ComponentSerialNo { get; set; }

        public int ModelId { get; set; }

        public int ConditionId { get; set; }

        public string? Reference { get; set; }

        public string? Details { get; set; }

        public decimal? Value { get; set; }

        public int LocationId { get; set; }

        public int PlanId { get; set; }

        public string? TenantId { get; set; }

        public DateTime? Date { get; set; }


    }
}
