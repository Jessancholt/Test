using Test.DataAccess.Extensions;
using Test.DataAccess.Repositories.Interfaces;
using Test.DataAccess.SqlCommands.Interfaces;
using Test.DataAccess.Wrappers.Interfaces;

namespace Test.DataAccess.Repositories
{
    public class SqlRepository<TEntity> : ISqlRepository<TEntity>
        where TEntity : class
    {
        protected readonly INonQueryWrapper<TEntity> NonQueryWrapper;

        protected readonly ISqlCommands<TEntity> Sql;

        public SqlRepository(INonQueryWrapper<TEntity> nonQueryWrapper, ISqlCommands<TEntity> sql)
        {
            NonQueryWrapper = nonQueryWrapper;
            Sql = sql;
        }

        public async Task Create(TEntity entity, CancellationToken cancellationToken)
        {
            var sql = Sql.Insert();
            var parameters = entity.GetSqliteParameters();
            await NonQueryWrapper.ExecuteNonQueryAsync(sql, parameters, cancellationToken);
        }
    }
}
