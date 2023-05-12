using Microsoft.Data.Sqlite;

namespace Test.DataAccess.Wrappers.Interfaces
{
    public interface INonQueryWrapper<TEntity> where TEntity : class
    {
        public Task ExecuteNonQueryAsync(string sql);

        public Task ExecuteNonQueryAsync(string sql, IEnumerable<SqliteParameter> parameters, CancellationToken cancellationToken);
    }
}
