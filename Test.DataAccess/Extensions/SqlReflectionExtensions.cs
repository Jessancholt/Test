using Microsoft.Data.Sqlite;

namespace Test.DataAccess.Extensions
{
    internal static class SqlReflectionExtensions
    {
        internal static TEntity GetEntity<TEntity>(this SqliteDataReader reader) where TEntity : class, new()
        {
            var entity = new TEntity();
            foreach (var p in entity.GetType().GetProperties())
            {
                var value = reader[p.Name];
                if (p.PropertyType.IsEnum)
                    value = Enum.Parse(p.PropertyType, value.ToString());

                p.SetValue(entity, value != DBNull.Value ? value : default);
            }

            return entity;
        }

        internal static IEnumerable<SqliteParameter> GetSqliteParameters<TEntity>(this TEntity entity) where TEntity : class
        {
            var parameters = entity.GetType()
                .GetProperties()
                .Select(p =>
                {
                    var value = new SqliteParameter($"@{p.Name}", p.GetValue(entity, null) ?? DBNull.Value);
                    return value;
                })
                .ToList();

            return parameters;
        }

        internal static string GetSqlitePropertyType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return "INTEGER";

                case TypeCode.String:
                    return "TEXT";

                default:
                    throw new ArgumentException("There is no such type in database");
            }
        }
    }
}
