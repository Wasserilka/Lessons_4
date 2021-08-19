using System;

namespace Lesson_2.Requests
{
    public class CreateJobRequest
    {
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public double PricePerHour { get; set; }
    }
}
