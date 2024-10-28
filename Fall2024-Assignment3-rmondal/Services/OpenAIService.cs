using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Fall2024_Assignment3_rmondal.ViewModels;

namespace Fall2024_Assignment3_rmondal.Services
{
    public class OpenAIService
    {
        private readonly string _endpoint;
        private readonly string _apiKey;
        private readonly string _deploymentName;
        private readonly string _apiVersion;

        public OpenAIService(IOptions<AzureOpenAISettings> settings)
        {
            _endpoint = settings.Value.Endpoint;
            _apiKey = settings.Value.Key;
            _deploymentName = settings.Value.DeploymentName;
            _apiVersion = settings.Value.ApiVersion;
        }

        public async Task<List<TweetViewModel>> GetActorTweetsAsync(string actorName, int tweetCount = 20)
        {
            var prompt = $"Generate {tweetCount} tweets related to the actor '{actorName}'. Each tweet should be on a new line and no longer than 280 characters.";
            var tweets = await CallAzureOpenAIApiAsync(prompt);

            var tweetViewModels = new List<TweetViewModel>();
            foreach (var tweet in tweets.Take(tweetCount))
            {
                var sentiment = await AnalyzeSentimentAsync(tweet);
                tweetViewModels.Add(new TweetViewModel { Content = tweet.Trim(), Sentiment = sentiment });
            }

            return tweetViewModels;
        }

        public async Task<List<MovieReviewViewModel>> GetMovieReviewsAsync(string movieTitle, int reviewCount = 10)
        {
            var prompt = $"Generate {reviewCount} reviews for the movie '{movieTitle}'. Each review should be on a new line.";
            var reviews = await CallAzureOpenAIApiAsync(prompt);

            var reviewViewModels = new List<MovieReviewViewModel>();
            foreach (var review in reviews.Take(reviewCount))
            {
                var sentiment = await AnalyzeSentimentAsync(review);
                reviewViewModels.Add(new MovieReviewViewModel { Content = review.Trim(), Sentiment = sentiment });
            }

            return reviewViewModels;
        }

        public async Task<string> GetOverallSentimentAsync(List<MovieReviewViewModel> reviews)
        {
            var sentimentPrompt = $"Analyze the overall sentiment of the following movie reviews. Respond with only 'Positive', 'Negative', or 'Neutral':\n\n{string.Join("\n", reviews.Select(r => r.Content))}";
            var result = await CallAzureOpenAIApiAsync(sentimentPrompt);
            return result.FirstOrDefault()?.Trim() ?? "Neutral";
        }

        private async Task<string> AnalyzeSentimentAsync(string text)
        {
            var prompt = $"Analyze the sentiment of the following text. Respond with only 'Positive', 'Negative', or 'Neutral':\n\n{text}";
            var result = await CallAzureOpenAIApiAsync(prompt);
            return result.FirstOrDefault()?.Trim() ?? "Neutral";
        }

        public async Task<string> GetOverallSentimentAsync(List<TweetViewModel> tweets)
        {
            var sentimentPrompt = $"Analyze the overall sentiment of the following tweets. Respond with only 'Positive', 'Negative', or 'Neutral':\n\n{string.Join("\n", tweets.Select(t => t.Content))}";
            var result = await CallAzureOpenAIApiAsync(sentimentPrompt);
            return result.FirstOrDefault()?.Trim() ?? "Neutral";
        }

        public async Task<SentimentAnalysisModel> GetSentimentAnalysisAsync(List<string> texts)
        {
            var sentimentPrompt = $"Analyze the overall sentiment of the following texts. Respond with only 'Positive', 'Negative', or 'Neutral':\n\n{string.Join("\n", texts)}";
            var result = await CallAzureOpenAIApiAsync(sentimentPrompt);

            var sentiment = result.FirstOrDefault()?.Trim();
            return new SentimentAnalysisModel
            {
                Sentiment = sentiment ?? "Neutral"
            };
        }

        private async Task<List<string>> CallAzureOpenAIApiAsync(string prompt)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("api-key", _apiKey);

                var requestContent = new StringContent(JsonConvert.SerializeObject(new
                {
                    messages = new[]
                    {
                        new { role = "system", content = "You are a helpful assistant." },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 1000,
                    temperature = 0.7
                }), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{_endpoint}openai/deployments/{_deploymentName}/chat/completions?api-version=2024-02-15-preview", requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error calling Azure OpenAI API: {response.StatusCode} - {response.ReasonPhrase}\nError details: {errorContent}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<JObject>(responseBody);
                var choices = result["choices"] as JArray;

                return choices.Select(choice => choice["message"]["content"].ToString()).ToList();
            }
        }
    }

    public class TweetModel
    {
        public string Content { get; set; }
    }

    public class SentimentAnalysisModel
    {
        public string Sentiment { get; set; }
    }
}