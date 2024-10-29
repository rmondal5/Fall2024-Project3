using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Fall2024_Assignment3_rmondal.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        [Url]
        public string IMDBLink { get; set; }

        [Url]
        public string Photo { get; set; }

        // Navigation property for Movies
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
