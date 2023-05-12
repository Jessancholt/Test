using Test.HackerNewsProvider.Models;

namespace Test.HackerNewsProvider.Clients.Interfaces
{
    public interface IHackerNewsApi
    {
        public Task<List<HackerNewsResponse>> GetNewsAsync(CancellationToken cancellationToken);
    }
}
