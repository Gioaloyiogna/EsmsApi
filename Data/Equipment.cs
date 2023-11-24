﻿using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data;

public partial class Equipment
{
    public int Id { get; set; }

    public int? ModelId { get; set; }

    public string EquipmentId { get; set; } = null!;

    public string? Description { get; set; }

    public string? SerialNumber { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public DateTime? EndOfLifeDate { get; set; }

    public string? Facode { get; set; }

    public string? Note { get; set; }

    public DateTime? WarrantyStartDate { get; set; }

    public DateTime? WarrantyEndDate { get; set; }

    public string? UniversalCode { get; set; }

    public string? MeterType { get; set; }

    public string? TenantId { get; set; }

    public int? Category { get; set; }

    public string Status { get; set; } = null!;

    public int InitialReading { get; set; }

    public int Adjustment { get; set; }

    public DateTime? SiteArrivalDate { get; set; }

    public DateTime? ComissionDate { get; set; }

    public string? EquipmentPicture { get; set; }

    public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();

    public virtual ICollection<Backlog> Backlogs { get; set; } = new List<Backlog>();

    public virtual Category? CategoryNavigation { get; set; }

    public virtual ICollection<DefectEntry> DefectEntries { get; set; } = new List<DefectEntry>();

    public virtual ICollection<DrillEntry> DrillEntries { get; set; } = new List<DrillEntry>();

    public virtual ICollection<EquipmentComponentSchedule> EquipmentComponentSchedules { get; set; } = new List<EquipmentComponentSchedule>();

    public virtual ICollection<GroundEngTool> GroundEngTools { get; set; } = new List<GroundEngTool>();

    public virtual ICollection<HoursEntry> HoursEntries { get; set; } = new List<HoursEntry>();

    public virtual ICollection<HoursEntryTemp> HoursEntryTemps { get; set; } = new List<HoursEntryTemp>();

    public virtual ICollection<LubeDispensing> LubeDispensings { get; set; } = new List<LubeDispensing>();

    public virtual ICollection<LubeEntry> LubeEntries { get; set; } = new List<LubeEntry>();

    public virtual Model? Model { get; set; }

    public virtual ICollection<ProDrill> ProDrills { get; set; } = new List<ProDrill>();

    public virtual ICollection<ProFuelIntake> ProFuelIntakes { get; set; } = new List<ProFuelIntake>();

    public virtual ICollection<ProhaulerUnit> ProhaulerUnits { get; set; } = new List<ProhaulerUnit>();

    public virtual ICollection<ProloaderUnit> ProloaderUnits { get; set; } = new List<ProloaderUnit>();
}
