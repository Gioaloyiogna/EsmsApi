using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class ComponentCondition
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Tracker> Trackers { get; set; } = new List<Tracker>();

    public virtual ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
}
