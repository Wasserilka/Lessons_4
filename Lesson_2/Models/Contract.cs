namespace Timesheets.Models
{
    public class Contract
    {
        private long _id;
        private string _name;
        private long _customerId;

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
        public long CustomerId
        {
            get => _customerId;
            set => _customerId = value;
        }

        internal Contract(long id, string name, long customerId)
        {
            _id = id;
            _name = name;
            _customerId = customerId;
        }

        internal Contract() {}
    }

    public class ContractFactory
    {
        public Contract Create(long id, string name, long customerId) => new Contract(id, name, customerId);
    }
}
