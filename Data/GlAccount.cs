using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class GlAccount
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? TenantId { get; set; }

    public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
}
