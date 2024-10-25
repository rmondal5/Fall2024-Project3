using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic; // For List<T>
using System.Linq; // For Select
using System.Threading.Tasks; // For async Task


namespace Fall2024_Assignment3_rmondal.Services
{
    public class OpenAIService
    {
        private readonly string _apiKey;

        public OpenAIService(IConfiguration configuration)
        {
            // Correctly accessing the OpenAI API key from configuration
            _apiKey = configuration["23c29d47e0414bd5a3c8917df023bfea"];
        }

        public async Task<List<ReviewModel>> GetMovieReviewsAsync(string movieTitle)
        {
            // Implement logic to call OpenAI API for generating movie reviews
            var prompt = $"Generate 10 movie reviews for the movie titled '{movieTitle}'.";
            var reviews = await CallOpenAIApiAsync(prompt);

            return reviews.Select(review => new ReviewModel { Content = review }).ToList();
        }

        public async Task<List<TweetModel>> GetActorTweetsAsync(string actorName)
        {
            // Implement logic to call OpenAI API for generating tweets related to the actor
            var prompt = $"Generate 20 tweets related to the actor '{actorName}'.";
            var tweets = await CallOpenAIApiAsync(prompt);

            return tweets.Select(tweet => new TweetModel { Content = tweet }).ToList();
        }

        public async Task<SentimentAnalysisModel> GetSentimentAnalysisAsync(List<string> texts)
        {
            // Implement logic to analyze the sentiment of the reviews or tweets
            var sentimentPrompt = $"Analyze the sentiment of the following texts: {string.Join(" ", texts)}.";
            var result = await CallOpenAIApiAsync(sentimentPrompt);

            // Assume OpenAI API returns sentiment and score
            return new SentimentAnalysisModel
            {
                Sentiment = result.FirstOrDefault(), // Mocked sentiment (e.g., "positive", "negative", "neutral")
                Score = 0.85f // Mocked score (can be any score or return from OpenAI)
            };
        }

        private async Task<List<string>> CallOpenAIApiAsync(string prompt)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                var requestContent = new StringContent(JsonConvert.SerializeObject(new
                {
                    model = "text-davinci-003", // Specify the model you want to use
                    prompt = prompt,
                    max_tokens = 100,
                    temperature = 0.7
                }), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/completions", requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error calling OpenAI API: {response.ReasonPhrase}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseBody);
                var choices = result.choices as JArray;

                return choices.Select(choice => (string)choice["text"]).ToList();
            }
        }
    }
}
