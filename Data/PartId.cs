using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class PartId
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tenant { get; set; }

    public virtual ICollection<GroundEngTool> GroundEngTools { get; set; } = new List<GroundEngTool>();
}
