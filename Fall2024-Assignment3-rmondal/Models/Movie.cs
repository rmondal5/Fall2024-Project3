using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fall2024_Assignment3_rmondal.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [Url]
        public string IMDBLink { get; set; }

        public string? Genre { get; set; }

        [Range(1888, 2100, ErrorMessage = "Year must be between 1888 and 2100.")]
        public int? Year { get; set; }

        [Url]
        public string? Poster { get; set; }

        // Navigation property for Actors
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
