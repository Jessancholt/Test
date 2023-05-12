namespace Test.DataAccess.Providers.Interfaces
{
    public interface ISqlProvider<TEntity>
        where TEntity : new()
    {
        public Task<IList<TEntity>> Get(CancellationToken cancellationToken);

        public Task<TEntity?> Get(int id, CancellationToken cancellationToken);
    }
}
