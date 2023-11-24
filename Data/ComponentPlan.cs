using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class ComponentPlan
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? TenantId { get; set; }
}
