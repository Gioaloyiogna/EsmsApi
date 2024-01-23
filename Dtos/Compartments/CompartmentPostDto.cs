namespace ServiceManagerApi.Dtos.Compartments
{
    public class CompartmentPostDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? TenantId { get; set; }
    }
}
