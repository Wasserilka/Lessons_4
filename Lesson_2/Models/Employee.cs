namespace Timesheets.Models
{
    public class Employee
    {
        private long _id;
        private string _name;

        public long Id
        {
            get => _id;
            set => _id = value;
        }
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        internal Employee(long id, string name)
        {
            _id = id;
            _name = name;
        }

        internal Employee() { }
    }

    public class EmployeeFactory
    {
        public Employee Create(long id, string name) => new Employee(id, name);
    }
}
