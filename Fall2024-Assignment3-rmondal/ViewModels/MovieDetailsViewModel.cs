using System.Collections.Generic;
using Fall2024_Assignment3_rmondal.Models;

namespace Fall2024_Assignment3_rmondal.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; }
        public List<Actor> Actors { get; set; }
        public List<MovieReviewViewModel> Reviews { get; set; }
        public string OverallSentiment { get; set; }
    }

    public class MovieReviewViewModel
    {
        public string Content { get; set; }
        public string Sentiment { get; set; }
    }
}