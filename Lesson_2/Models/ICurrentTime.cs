using System;

namespace Timesheets.Models
{
    public interface ICurrentTime
    {
        DateTime UtcNow();
    }

    public class SystemTime : ICurrentTime
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }

    public class TimeForTests : ICurrentTime
    {
        private readonly DateTime _now;

        public TimeForTests(DateTime now)
        {
            _now = now;
        }

        public DateTime UtcNow()
        {
            return _now;
        }
    }
}
