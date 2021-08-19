using Lesson_2.Models;

namespace Lesson_2.Responses
{
    public class InvoiceDto
    {
        public long Id { get; set; }
        public Contract Contract { get; set; }
        public double Price { get; set; }
    }
}
