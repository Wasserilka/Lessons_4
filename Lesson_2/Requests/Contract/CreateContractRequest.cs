using System;

namespace Lesson_2.Requests
{
    public class CreateContractRequest
    {
        public long JobId { get; set; }
        public long ClientId { get; set; }
    }
}
