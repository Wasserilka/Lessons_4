﻿namespace Lesson_2.Models
{
    public class Contract
    {
        public long Id { get; set; }
        public Job Job { get; set; }
        public Client Client { get; set; }
    }
}
