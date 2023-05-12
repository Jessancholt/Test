using Test.DataAccess.SqlCommands.Interfaces;
using Test.DataAccess.Wrappers.Interfaces;

namespace Test.DataAccess.Wrappers
{
    public class SeedWrapper<TEntity> : ISeedWrapper<TEntity> where TEntity : class
    {
        private readonly INonQueryWrapper<TEntity> _nonQueryWrapper;
        private readonly ISqlCommands<TEntity> _sql;

        public SeedWrapper(
            INonQueryWrapper<TEntity> nonQueryWrapper,
            ISqlCommands<TEntity> sql)
        {
            _nonQueryWrapper = nonQueryWrapper;
            _sql = sql;
        }

        public async Task CreateTable()
        {
            var sql = _sql.CreateTable();
            await _nonQueryWrapper.ExecuteNonQueryAsync(sql);
        }
    }
}
