using Test.BusinessLogic.Services.Interfaces;
using Test.DataAccess.Entities;
using Test.DataAccess.Providers.Interfaces;
using Test.DataAccess.Repositories.Interfaces;

namespace Test.BusinessLogic.Services
{
    public class PostsService : IPostsService
    {
        private readonly ISqlProvider<Post> _provider;
        private readonly ISqlRepository<Post> _repository;

        private readonly IHackerNewsService _hackerNewsService;

        public PostsService(
            ISqlProvider<Post> provider,
            ISqlRepository<Post> repository,
            IHackerNewsService hackerNewsService)
        {
            _provider = provider;
            _repository = repository;
            _hackerNewsService = hackerNewsService;
        }

        public async Task<IList<Post>> GetAsync(CancellationToken cancellationToken)
        {
            var posts = await _provider.Get(cancellationToken);
            if (posts.Count == 0)
            {
                posts = await _hackerNewsService.GetAsync(cancellationToken);

                await CreateRangeAsync(posts, cancellationToken);
            }

            return posts;
        }

        public async Task<Post?> GetAsync(int id, CancellationToken cancellationToken)
        {
            var post = await _provider.Get(id, cancellationToken);
            return post;
        }

        public async Task CreateAsync(Post entity, CancellationToken cancellationToken)
        {
            await _repository.Create(entity, cancellationToken);
        }

        public async Task CreateRangeAsync(IList<Post> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
                await _repository.Create(entity, cancellationToken);
        }
    }
}