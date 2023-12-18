using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class SystemType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string TenantId { get; set; } = null!;
}
