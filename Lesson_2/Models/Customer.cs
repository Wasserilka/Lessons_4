using System.Collections.Generic;

namespace Timesheets.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Contract> Contracts { get; set; }

        public Customer()
        {
            Contracts = new List<Contract>();
        }
    }
}
