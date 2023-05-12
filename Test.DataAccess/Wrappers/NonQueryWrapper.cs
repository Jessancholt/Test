using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Test.DataAccess.Settings;
using Test.DataAccess.Wrappers.Interfaces;

namespace Test.DataAccess.Wrappers
{
    public class NonQueryWrapper<TEntity> : INonQueryWrapper<TEntity>
        where TEntity : class
    {
        private readonly DbSettings _settings;

        public NonQueryWrapper(IOptions<DbSettings> options)
        {
            _settings = options.Value;
        }

        public async Task ExecuteNonQueryAsync(string sql)
        {
            await using var connection = new SqliteConnection(_settings.ConnectionString);
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = sql;
            await command.ExecuteNonQueryAsync();
        }

        public async Task ExecuteNonQueryAsync(string sql, IEnumerable<SqliteParameter> parameters, CancellationToken cancellationToken)
        {
            await using var connection = new SqliteConnection(_settings.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            await using var command = connection.CreateCommand();
            command.Parameters.AddRange(parameters);
            command.CommandText = sql;
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}
