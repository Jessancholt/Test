namespace Test.DataAccess.Repositories.Interfaces
{
    public interface ISqlRepository<TEntity>
        where TEntity : class
    {
        public Task Create(TEntity entity, CancellationToken cancellationToken);
    }
}
