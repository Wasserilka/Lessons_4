using Lesson_2.Models;

namespace Lesson_2.Responses
{
    public class ContractDto
    {
        public long Id { get; set; }
        public Job Job { get; set; }
        public Client Client { get; set; }
    }
}
