﻿using Timesheets.Models;
using System;
using System.Collections.Generic;

namespace Timesheets.Responses
{
    public class TaskDto
    {
        public long Id { get; set; }
        public List<long> Employees { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public decimal PricePerHour { get; set; }
        public bool IsClosed { get; set; }
    }
}
