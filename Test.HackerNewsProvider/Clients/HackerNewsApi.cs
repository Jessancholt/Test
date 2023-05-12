using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Test.HackerNewsProvider.Clients.Interfaces;
using Test.HackerNewsProvider.Models;
using Test.HackerNewsProvider.Settings;

namespace Test.HackerNewsProvider.Clients
{
    public class HackerNewsApi : IHackerNewsApi
    {
        private readonly HackerNewsClientSettings _settings;

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger _logger;

        public HackerNewsApi(
            IHttpClientFactory httpClientFactory,
            ILogger logger,
            IOptions<HackerNewsClientSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _settings = options.Value;
        }

        public async Task<List<HackerNewsResponse>> GetNewsAsync(CancellationToken cancellationToken)
        {
            var hackerNewsResponse = new List<HackerNewsResponse>();
            var topStories = await CreateRequest<List<int>>(_settings.TopStoriesUrl, HttpMethod.Get, cancellationToken);
            if (topStories is not null)
            {
                await Parallel.ForEachAsync(topStories.Take(_settings.NewsCount), cancellationToken,
                    async (storyId, token) =>
                {
                    var response = await GetStory(storyId, token);
                    if (response is not null)
                        hackerNewsResponse.Add(response);
                });
            }

            return hackerNewsResponse;
        }

        private async Task<HackerNewsResponse?> GetStory(int storyId, CancellationToken cancellationToken)
        {
            var endpoint = _settings.GetByIdUrl.Replace("{0}", storyId.ToString());
            var hackerNewsResult = await CreateRequest<HackerNewsResponse>(endpoint, HttpMethod.Get, cancellationToken);
            return hackerNewsResult;
        }

        private async Task<T?> CreateRequest<T>(string endpoint, HttpMethod httpMethod, CancellationToken cancellationToken) where T : new()
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var result = default(T);
                HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, _settings.BaseUrl + endpoint);
                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest, cancellationToken);
                string json = await httpResponse.Content?.ReadAsStringAsync(cancellationToken);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    try
                    {
                        var apiResponse = JsonConvert.DeserializeObject<T>(json);
                        result = apiResponse;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Deserialize apiResponse failed: {0}", json);
                        throw new ArgumentException(ex.Message);
                    }
                }
                else
                {
                    _logger.Error("Response is null: {0}", json);
                }

                return result;
            }
        }
    }
}