using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class BacklogStatusDeletePrevention
{
    public int? BacklogStatusId { get; set; }

    public virtual BacklogStatus? BacklogStatus { get; set; }
}
