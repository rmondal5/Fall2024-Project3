﻿using System.ComponentModel.DataAnnotations;

namespace Fall2024_Assignment3_rmondal.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        [Url]
        public string IMDBLink { get; set; }

        public string Photo { get; set; }

        // Navigation property for Movies
        public List<MovieActor> MovieActors { get; set; }
    }
}