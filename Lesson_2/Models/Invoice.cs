namespace Timesheets.Models
{
    public class Invoice
    {
        public long Id { get; set; }
        public Contract Contract { get; set; }
        public decimal Cost { get; set; }
    }
}
