using System.Collections.Generic;
using Fall2024_Assignment3_rmondal.Models;
using Fall2024_Assignment3_rmondal.Services;

namespace Fall2024_Assignment3_rmondal.ViewModels
{
    public class ActorDetailsViewModel
    {
        public Actor Actor { get; set; }
        public List<Movie> Movies { get; set; }
        public List<TweetViewModel> Tweets { get; set; }
        public string OverallSentiment { get; set; }
    }

    public class TweetViewModel
    {
        public string Content { get; set; }
        public string Sentiment { get; set; }
    }
}