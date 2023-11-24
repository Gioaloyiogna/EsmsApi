namespace ServiceManagerApi.Dtos.Component
{
    public class ComponentDto
    {
     

       
        public int ModelId { get; set; }
        public string? Name { get; set; }

        public int? ScheduledLifeHours { get; set; }

        public int ComponentClass { get; set; }

        public string? TenantId { get; set; }
    }
}
