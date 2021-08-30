using System;
using System.Collections.Generic;

namespace Timesheets.Models 
{
    public class Task
    {
        private long _id;
        private List<long> _employees;
        private DateTimeOffset _start;
        private DateTimeOffset _end;
        private decimal _pricePerHour;
        private bool _isClosed;

        public long Id
        {
            get => _id;
            set => _id = value;
        }
        public List<long> Employees
        {
            get => _employees;
            set => _employees = value;
        }
        public DateTimeOffset Start
        {
            get => _start;
            set => _start = value;
        }
        public DateTimeOffset End
        {
            get => _end;
            set => _end = value;
        }
        public decimal PricePerHour
        {
            get => _pricePerHour;
            set => _pricePerHour = value;
        }
        public bool IsClosed
        {
            get => _isClosed;
            set => _isClosed = value;
        }

        internal Task(long id, decimal pricePerHour)
        {
            _id = id;
            _start = DateTime.UtcNow;
            _pricePerHour = pricePerHour;
            Employees = new List<long>();
            _isClosed = false;
        }

        internal Task() { }

        public void Close()
        {
            if (IsClosed)
            {
                throw new AlreadyClosedException();
            }

            IsClosed = true;
            End = DateTime.UtcNow;
        }

        public decimal GetCost()
        {
            if (!IsClosed)
            {
                throw new NeedToBeClosedException();
            }

            return (decimal)((_end - _start).TotalSeconds / 3600) * _pricePerHour;
        }

        public void AddEmployee(long employeeId)
        {
            _employees.Add(employeeId);
        }

        public void RemoveEmployee(long employeeId)
        {
            _employees.Remove(employeeId);
        }
    }

    public class AlreadyClosedException : Exception { }
    public class NeedToBeClosedException : Exception { }

    public class TaskFactory
    {
        public Task Create(long id, decimal pricePerHour) => new Task(id, pricePerHour);
    }
}
