using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class BacklogStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string TenantId { get; set; } = null!;
}
