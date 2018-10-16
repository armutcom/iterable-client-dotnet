using Armut.Iterable.Client.Core.Responses;
using System.Threading.Tasks;

namespace Armut.Iterable.Client.Contracts
{
    public interface IRestClient
    {
        Task<ApiResponse<T>> GetAsync<T>(string path) where T : class, new();

        Task<ApiResponse<T>> PostAsync<T>(string path, object request) where T : class, new();

        Task<ApiResponse<T>> DeleteAsync<T>(string path) where T : class, new();

        Task<ApiResponse<T>> DeleteAsync<T>(string path, object request) where T : class, new();
    }
}