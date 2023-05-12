namespace Test.HackerNewsProvider.Settings
{
    public class HackerNewsClientSettings
    {
        public string? BaseUrl { get; set; }

        public string? TopStoriesUrl { get; set; }

        public string? GetByIdUrl { get; set; }

        public int NewsCount { get; set; }
    }
}
