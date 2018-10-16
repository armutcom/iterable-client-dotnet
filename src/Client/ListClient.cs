using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.ListModels;
using System.Threading.Tasks;

namespace Armut.Iterable.Client
{
    public class ListClient : IListClient
    {
        private readonly IRestClient _client;

        public ListClient(IRestClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse<CreateListResponse>> CreateAsync(string name)
        {
            return await _client.PostAsync<CreateListResponse>("/api/lists", name).ConfigureAwait(false);
        }

        public async Task<ApiResponse<DeleteListResponse>> DeleteAsync(int listId)
        {
            return await _client.DeleteAsync<DeleteListResponse>($"/api/lists/{listId}").ConfigureAwait(false);
        }

        public async Task<ApiResponse<GetAllListResponse>> GetAllListsAsync()
        {
            return await _client.GetAsync<GetAllListResponse>("/api/lists").ConfigureAwait(false);
        }

        public async Task<ApiResponse<object>> GetSizeAsync(int listId)
        {
            return await _client.GetAsync<object>($"/api/lists/{listId}/size").ConfigureAwait(false);
        }

        public async Task<ApiResponse<GetUsersResponse>> GetUsersAsync(int listId)
        {
            //var response = await _client.GetAsync<string>($"/api/lists/getUsers?listId={listId}").ConfigureAwait(false);

            throw new System.NotImplementedException();
        }

        public async Task<ApiResponse<SubscribeResponse>> SubscribeAsync(SubscribeRequest model)
        {
            return await _client.PostAsync<SubscribeResponse>("/api/lists/subscribe", model).ConfigureAwait(false);
        }

        public async Task<ApiResponse<UnsubscribeResponse>> UnsubscribeAsync(UnsubscribeRequest model)
        {
            return await _client.PostAsync<UnsubscribeResponse>("/api/lists/unsubscribe", model).ConfigureAwait(false);
        }
    }
}