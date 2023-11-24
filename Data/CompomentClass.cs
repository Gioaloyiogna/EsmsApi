using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class CompomentClass
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}
