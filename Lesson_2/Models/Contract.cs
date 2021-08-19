using System.Collections.Generic;

namespace Lesson_2.Models
{
    public class Contract
    {
        public long contractId { get; set; }
        public List<long> employees { get; set; }

        public Contract()
        {
            employees = new List<long>();
        }
    }
}
