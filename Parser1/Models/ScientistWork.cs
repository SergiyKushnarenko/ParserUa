﻿namespace Parser1.Models
{
    public class ScientistWork
    {
        public int ScientistId { get; set; }
        public Scientist? Scientist { get; set; }

        public int WorkId { get; set; }

        public Work? Work { get; set; }
    }
}
