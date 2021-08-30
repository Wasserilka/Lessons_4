using System.Collections.Generic;

namespace Timesheets.Models
{
    public class Customer
    {
        private long _id;
        private string _name;
        private List<Contract> _contracts;

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
        public List<Contract> Contracts
        {
            get => _contracts;
            set => _contracts = value;
        }

        internal Customer(long id, string name)
        {
            _id = id;
            _name = name;
            Contracts = new List<Contract>();
        }

        internal Customer() { }
    }
    public class CustomerFactory
    {
        public Customer Create(long id, string name) => new Customer(id, name);
    }
}
