using Test.DataAccess.Entities;

namespace Test.BusinessLogic.Services.Interfaces
{
    public interface IHackerNewsService
    {
        public Task<IList<Post>> GetAsync(CancellationToken cancellationToken);
    }
}
