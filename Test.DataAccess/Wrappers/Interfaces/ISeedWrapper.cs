namespace Test.DataAccess.Wrappers.Interfaces
{
    public interface ISeedWrapper<TEntity> where TEntity : class
    {
        public Task CreateTable();
    }
}
