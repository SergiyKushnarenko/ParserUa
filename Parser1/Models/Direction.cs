﻿namespace Parser1.Models
{
    public class Direction
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Subdirection> Subdirections { get; set; }
    }
}