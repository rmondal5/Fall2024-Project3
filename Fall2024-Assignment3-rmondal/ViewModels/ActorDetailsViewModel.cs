using System.Collections.Generic;
using Fall2024_Assignment3_rmondal.Models;

namespace Fall2024_Assignment3_rmondal.ViewModels
{
    public class ActorDetailsViewModel
    {
        public Actor Actor { get; set; }
        public List<Movie> Movies { get; set; }
        public List<TweetSentiment> Tweets { get; set; } // Ensure this matches the type returned by GetAITweets
        public string OverallSentiment { get; set; }
    }

    public class TweetSentiment
    {
        public string Text { get; set; }
        public string Sentiment { get; set; }
    }

}
