namespace Lesson_2.Models
{
    public class Invoice
    {
        public long Id { get; set; }
        public Contract Contract { get; set; }
        public double Price { get; set; }

        public void GetPrice()
        {
            var hours = (Contract.Job.Start.ToUnixTimeSeconds() - Contract.Job.Start.ToUnixTimeSeconds()) / 3600;
            Price = hours * Contract.Job.PricePerHour;
        }
    }
}
