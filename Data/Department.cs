using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? TenanId { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
}
