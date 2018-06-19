using System.Threading.Tasks;

namespace Atgo2.Api.CrossCuttingLayer.Caching.Interfaces
{
    public interface ICacheService
    {
        Task SetAsync(string key, object value);
        Task<T> GetAsync<T>(string key);
        Task DeleteAsync(string key);
        void Set(string key, object value);
        T Get<T>(string key);
        void Delete(string key);
        void Clear();
    }
}
