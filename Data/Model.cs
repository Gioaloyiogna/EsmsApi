﻿using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class Model
{
    public int ModelId { get; set; }

    public int? ManufacturerId { get; set; }

    public int? ModelClassId { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? PictureLink { get; set; }

    public string? TenantId { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public virtual ICollection<EquipmentComponentSchedule> EquipmentComponentSchedules { get; set; } = new List<EquipmentComponentSchedule>();

    public virtual ICollection<LubeConfig> LubeConfigs { get; set; } = new List<LubeConfig>();

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ModelClass? ModelClass { get; set; }

    public virtual ICollection<Tracker> Trackers { get; set; } = new List<Tracker>();
}
