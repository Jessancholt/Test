namespace Test.HackerNewsProvider.Models
{
    public class HackerNewsResponse
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? By { get; set; }

        public long Time { get; set; }

        public string? Url { get; set; }
    }
}
