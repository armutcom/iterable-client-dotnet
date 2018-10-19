using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.ListModels;
using System.Threading.Tasks;

namespace Armut.Iterable.Client.Contracts
{
    public interface IListClient
    {
        Task<ApiResponse<GetAllListResponse>> GetAllListsAsync();

        Task<ApiResponse<GetUsersResponse>> GetUsersAsync(int listId);

        Task<ApiResponse<GetSizeResponse>> GetSizeAsync(int listId);

        Task<ApiResponse<CreateListResponse>> CreateAsync(string name);

        Task<ApiResponse<DeleteListResponse>> DeleteAsync(int listId);

        Task<ApiResponse<SubscribeResponse>> SubscribeAsync(SubscribeRequest model);

        Task<ApiResponse<UnsubscribeResponse>> UnsubscribeAsync(UnsubscribeRequest model);
    }
}