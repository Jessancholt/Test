using Test.DataAccess.Entities;

namespace Test.BusinessLogic.Services.Interfaces
{
    public interface IPostsService
    {
        public Task<IList<Post>> GetAsync(CancellationToken cancellationToken);

        public Task<Post?> GetAsync(int id, CancellationToken cancellationToken);

        public Task CreateAsync(Post entity, CancellationToken cancellationToken);
    }
}
