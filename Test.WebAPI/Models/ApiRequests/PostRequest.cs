namespace Test.WebAPI.Models.ApiRequests
{
    public class PostRequest
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? By { get; set; }

        public string? Time { get; set; }

        public string? Url { get; set; }
    }
}
