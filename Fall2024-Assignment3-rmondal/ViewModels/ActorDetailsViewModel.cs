using System.Collections.Generic;
using Fall2024_Assignment3_rmondal.Models;

namespace Fall2024_Assignment3_rmondal.ViewModels
{
    public class ActorDetailsViewModel
    {
        public Actor Actor { get; set; }
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<TweetSentiment> Tweets { get; set; } = new List<TweetSentiment>();
        public string OverallSentiment { get; set; }

        public class TweetSentiment
        {
            public string Tweet { get; set; }
            public string Sentiment { get; set; }
        }
    }
}
