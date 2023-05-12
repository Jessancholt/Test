namespace Test.DataAccess.SqlCommands.Interfaces
{
    public interface ISqlCommands<TEntity>
        where TEntity : class
    {
        public string CreateTable();

        public string Insert();

        public string Select();

        public string SelectById(int id);
    }
}
