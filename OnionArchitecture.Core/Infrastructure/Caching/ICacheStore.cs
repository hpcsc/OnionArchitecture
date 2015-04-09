
namespace OnionArchitecture.Core.Infrastructure.Caching
{
    public interface ICacheStore
    {
        void Remove(string key);
        void Store(string key, object data);
        T Retrieve<T>(string storageKey) where T : class;
    }
}
