using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Test.DataAccess.Extensions;
using Test.DataAccess.Settings;
using Test.DataAccess.Wrappers.Interfaces;

namespace Test.DataAccess.Wrappers
{
    public class DbProviderWrapper<TEntity> : IDbProviderWrapper<TEntity>
        where TEntity : class, new()
    {
        private readonly DbSettings _settings;

        public DbProviderWrapper(IOptions<DbSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<IList<TEntity>> ExecuteReaderAsync(string sql, CancellationToken cancellationToken)
        {
            await using var connection = new SqliteConnection(_settings.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            await using var command = connection.CreateCommand();
            command.CommandText = sql;

            var reader = await command.ExecuteReaderAsync(cancellationToken);
            var entities = new List<TEntity>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var entity = reader.GetEntity<TEntity>();
                    entities.Add(entity);
                }
            }

            return entities;
        }

        public async Task<object?> ExecuteScalarAsync(string sql, CancellationToken cancellationToken)
        {
            await using var connection = new SqliteConnection(_settings.ConnectionString);
            await connection.OpenAsync(cancellationToken);
            await using var command = connection.CreateCommand();
            command.CommandText = sql;

            return await command.ExecuteScalarAsync(cancellationToken);
        }
    }
}
