using System.Collections.Generic;
using System.Threading.Tasks;
using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Models.ListModels;

namespace Armut.Iterable.Client
{
    public class ListClient : IListClient
    {
        private readonly IRestClient _client;

        public ListClient(IRestClient client)
        {
            _client = client;
        }

        public async Task<int> CreateAsync(string name)
        {
            return await _client.PostAsync<int>("/api/lists", name).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int listId)
        {
            await _client.DeleteAsync($"/api/lists/{listId}").ConfigureAwait(false);
        }

        public async Task<GetAllListResponse> GetAllListsAsync()
        {
            return await _client.GetAsync<GetAllListResponse>("/api/lists").ConfigureAwait(false);
        }

        public async Task<long> GetSizeAsync(int listId)
        {
            return await _client.GetAsync<long>($"/api/lists/{listId}/size").ConfigureAwait(false);
        }

        public async Task<IEnumerable<string>> GetUsersAsync(int listId)
        {
            return await _client.GetAsync<IEnumerable<string>>($"/api/lists/getUsers?listId={listId}").ConfigureAwait(false);
        }

        public async Task<SubscribeResponse> SubscribeAsync(SubscribeRequest model)
        {
            return await _client.PostAsync<SubscribeResponse>("/api/lists/subscribe", model).ConfigureAwait(false);
        }

        public async Task<UnsubscribeResponse> UnsubscribeAsync(UnsubscribeRequest model)
        {
            return await _client.PostAsync<UnsubscribeResponse>("/api/lists/unsubscribe", model).ConfigureAwait(false);
        }
    }
}