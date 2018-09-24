using System.Threading.Tasks;

namespace Armut.Iterable.Client.Contracts
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string url);

        Task PostAsync<T>(string url, T request);

        Task DeleteAsync(string url);

        Task DeleteAsync<T>(string url, T request);
    }
}