using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class LubeDispensingSource
{
    public string Name { get; set; } = null!;

    public int Id { get; set; }

    public string TenantId { get; set; } = null!;
}
