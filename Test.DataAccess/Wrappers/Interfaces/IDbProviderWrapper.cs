namespace Test.DataAccess.Wrappers.Interfaces
{
    public interface IDbProviderWrapper<TEntity> where TEntity : class, new()
    {
        public Task<IList<TEntity>> ExecuteReaderAsync(string sql, CancellationToken cancellationToken);

        public Task<object?> ExecuteScalarAsync(string sql, CancellationToken cancellationToken);
    }
}
