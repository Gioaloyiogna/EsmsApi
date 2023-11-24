using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class LubeDispensingReason
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<LubeDispensing> LubeDispensings { get; set; } = new List<LubeDispensing>();
}
