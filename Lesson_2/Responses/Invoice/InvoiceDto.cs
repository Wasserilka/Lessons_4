using Timesheets.Models;

namespace Timesheets.Responses
{
    public class InvoiceDto
    {
        public long Id { get; set; }
        public Contract Contract { get; set; }
        public decimal Cost { get; set; }
    }
}
