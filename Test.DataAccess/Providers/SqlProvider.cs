using Test.DataAccess.Providers.Interfaces;
using Test.DataAccess.SqlCommands.Interfaces;
using Test.DataAccess.Wrappers.Interfaces;

namespace Test.DataAccess.Providers
{
    public class SqlProvider<TEntity> : ISqlProvider<TEntity>
        where TEntity : class, new()
    {
        private readonly IDbProviderWrapper<TEntity> _dbProvider;

        private readonly ISqlCommands<TEntity> _sql;

        public SqlProvider(IDbProviderWrapper<TEntity> dbProvider, ISqlCommands<TEntity> sql)
        {
            _dbProvider = dbProvider;
            _sql = sql;
        }

        public async Task<IList<TEntity>> Get(CancellationToken cancellationToken)
        {
            var sql = _sql.Select();
            var entities = await _dbProvider.ExecuteReaderAsync(sql, cancellationToken);

            return entities;
        }

        public async Task<TEntity?> Get(int id, CancellationToken cancellationToken)
        {
            var sql = _sql.SelectById(id);
            var entities = await _dbProvider.ExecuteReaderAsync(sql, cancellationToken);

            return entities.FirstOrDefault();
        }
    }
}
