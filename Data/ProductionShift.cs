﻿using System;
using System.Collections.Generic;

namespace ServiceManagerApi.Data
{
    public partial class ProductionShift
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Duration { get; set; }
    }
}