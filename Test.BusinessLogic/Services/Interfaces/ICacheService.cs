namespace Test.BusinessLogic.Services.Interfaces
{
    public interface ICacheService<TKey, TValue>
    {
        public Task<TValue?> Get(TKey key, Func<Task<TValue>> action);

        public Task<IList<TValue>> Get(TKey key, Func<Task<IList<TValue>>> action);
    }
}
