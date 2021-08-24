using Timesheets.Models;
using System.Collections.Generic;

namespace Timesheets.Responses
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Contract> Contracts { get; set; }
        public CustomerDto()
        {
            Contracts = new List<Contract>();
        }
    }
}
