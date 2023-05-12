using Test.DataAccess.Extensions;
using Test.DataAccess.SqlCommands.Interfaces;

namespace Test.DataAccess.SqlCommands
{
    public class SqlCommands<TEntity> : ISqlCommands<TEntity>
        where TEntity : class
    {
        public string CreateTable()
        {
            var parametersWithTypes = typeof(TEntity)
                .GetProperties()
                .Select(p => $"{p.Name} {p.PropertyType.GetSqlitePropertyType()}")
                .ToList();
            return $"CREATE TABLE IF NOT EXISTS {typeof(TEntity).Name}s ({string.Join(", ", parametersWithTypes)})";
        }

        public string Insert()
        {
            var parameters = typeof(TEntity)
                .GetProperties()
                .Select(p => $"{p.Name}")
                .ToList();
            var parametersWithAt = parameters.Select(x => "@" + x).ToList();
            return $"INSERT INTO {typeof(TEntity).Name}s ({string.Join(", ", parameters)}) VALUES ({string.Join(", ", parametersWithAt)})";
        }

        public string Select()
        {
            return $"SELECT * FROM {typeof(TEntity).Name}s";
        }

        public string SelectById(int id)
        {
            return $"SELECT * FROM {typeof(TEntity).Name}s WHERE Id = {id}";
        }
    }
}
