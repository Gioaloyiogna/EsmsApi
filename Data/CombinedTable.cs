using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class CombinedTable
{
    public string? ModelName { get; set; }

    public TimeSpan? Timed { get; set; }
}
