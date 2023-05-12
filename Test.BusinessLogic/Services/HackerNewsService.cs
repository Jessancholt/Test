using System.Globalization;
using Test.BusinessLogic.Services.Interfaces;
using Test.DataAccess.Entities;
using Test.HackerNewsProvider.Clients.Interfaces;

namespace Test.BusinessLogic.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly IHackerNewsApi _hackerNewsApi;

        public HackerNewsService(IHackerNewsApi hackerNewsApi)
        {
            _hackerNewsApi = hackerNewsApi;
        }

        public async Task<IList<Post>> GetAsync(CancellationToken cancellationToken)
        {
            var postsResponse = await _hackerNewsApi.GetNewsAsync(cancellationToken);
            var postModels = postsResponse.Select((x, index) => new Post()
            {
                Id = index + 1,
                Title = x.Title,
                By = x.By,
                Time = DateTimeOffset.FromUnixTimeSeconds(x.Time).DateTime.ToString(CultureInfo.CurrentCulture),
                Url = x.Url
            }).ToList();

            return postModels;
        }
    }
}
