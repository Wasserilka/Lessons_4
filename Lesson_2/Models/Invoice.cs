namespace Timesheets.Models
{
    public class Invoice
    {
        private long _id;
        private Contract _contract;
        private decimal _cost;

        public long Id
        {
            get => _id;
            set => _id = value;
        }
        public Contract Contract
        {
            get => _contract;
            set => _contract = value;
        }
        public decimal Cost
        {
            get => _cost;
            set => _cost = value;
        }

        internal Invoice(long id, Contract contract, decimal cost)
        {
            _id = id;
            _contract = contract;
            _cost = cost;
        }

        internal Invoice() { }
    }

    public class InvoiceFactory
    {
        public Invoice Create(long id, Contract contract, decimal cost) => new Invoice(id, contract, cost);
    }
}
