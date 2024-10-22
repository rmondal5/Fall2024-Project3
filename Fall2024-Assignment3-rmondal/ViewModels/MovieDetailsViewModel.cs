using System.Collections.Generic;
using Fall2024_Assignment3_rmondal.Models;

namespace Fall2024_Assignment3_rmondal.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; } // The movie details
        public List<Actor> Actors { get; set; } = new List<Actor>(); // List of actors associated with the movie
        public List<Review> Reviews { get; set; } = new List<Review>(); // List of AI-generated reviews
        public string OverallSentiment { get; set; } // Overall sentiment of the reviews
    }

    public class Review
    {
        public string Content { get; set; }
        public string Sentiment { get; set; }
    }
}
