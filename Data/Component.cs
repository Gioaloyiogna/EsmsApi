using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class Component
{
    public int Id { get; set; }

    public int ModelId { get; set; }

    public string? Name { get; set; }

    public int? ScheduledLifeHours { get; set; }

    public int ComponentClass { get; set; }

    public string? TenantId { get; set; }

    public virtual CompomentClass ComponentClassNavigation { get; set; } = null!;

    public virtual ICollection<EquipmentComponentSchedule> EquipmentComponentSchedules { get; set; } = new List<EquipmentComponentSchedule>();

    public virtual Model Model { get; set; } = null!;
}
