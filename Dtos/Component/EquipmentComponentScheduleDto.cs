namespace ServiceManagerApi.Dtos.Component
{
    public class EquipmentComponentScheduleDto
    {
        
         
        public string EquipmentId { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int ModelId { get; set; }

        public int ComponentId { get; set; }
        public string? TenantId { get; set; }

        public int ExpectedLife { get; set; }
    }
}
