using System;
using System.Collections.Generic;

namespace Lesson_2.Models
{
    public class Job
    {
        public long Id { get; set; }
        public List<Employee> Employees {get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public double PricePerHour { get; set; }
    }
}
