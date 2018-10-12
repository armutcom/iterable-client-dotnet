using System.Threading.Tasks;

namespace Armut.Iterable.Client.Contracts
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string path);

        Task<T> PostAsync<T>(string path, object request);

        Task<T> DeleteAsync<T>(string path);

        Task<T> DeleteAsync<T>(string path, object request);
    }
}