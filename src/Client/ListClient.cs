using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.ListModels;
using System.Net;
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
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return await _client.PostAsync<CreateListResponse>("/api/lists", name).ConfigureAwait(false);
        }

        public async Task<ApiResponse<DeleteListResponse>> DeleteAsync(int listId)
        {
            Ensure.GreaterThanZero(listId, nameof(listId));

            return await _client.DeleteAsync<DeleteListResponse>($"/api/lists/{listId}").ConfigureAwait(false);
        }

        public async Task<ApiResponse<GetAllListResponse>> GetAllListsAsync()
        {
            return await _client.GetAsync<GetAllListResponse>("/api/lists").ConfigureAwait(false);
        }

        public async Task<ApiResponse<GetSizeResponse>> GetSizeAsync(int listId)
        {
            Ensure.GreaterThanZero(listId, nameof(listId));

            ApiResponse apiResponse = await _client.GetContentAsync($"/api/lists/{listId}/size").ConfigureAwait(false);

            var response = new ApiResponse<GetSizeResponse>
            {
                HttpStatusCode = apiResponse.HttpStatusCode,
                Headers = apiResponse.Headers,
                UrlPath = apiResponse.UrlPath
            };

            if (apiResponse.HttpStatusCode != HttpStatusCode.BadRequest
                && apiResponse.HttpStatusCode != HttpStatusCode.Unauthorized
                && !string.IsNullOrEmpty(apiResponse.Content)
                && long.TryParse(apiResponse.Content, out long size))
            {
                response.Model = new GetSizeResponse
                {
                    Size = size
                };
            }

            return response;
        }

        public async Task<ApiResponse<GetUsersResponse>> GetUsersAsync(int listId)
        {
            Ensure.GreaterThanZero(listId, nameof(listId));

            ApiResponse apiResponse = await _client.GetContentAsync($"/api/lists/getUsers?listId={listId}").ConfigureAwait(false);

            var response = new ApiResponse<GetUsersResponse>
            {
                HttpStatusCode = apiResponse.HttpStatusCode,
                Headers = apiResponse.Headers,
                UrlPath = apiResponse.UrlPath
            };

            if (apiResponse.HttpStatusCode != HttpStatusCode.BadRequest
                && apiResponse.HttpStatusCode != HttpStatusCode.Unauthorized
                && !string.IsNullOrEmpty(apiResponse.Content))
            {
                response.Model = new GetUsersResponse
                {
                    UserIds = apiResponse.Content.Split('\n')
                };
            }

            return response;
        }

        public async Task<ApiResponse<SubscribeResponse>> SubscribeAsync(SubscribeRequest model)
        {
            Ensure.ArgumentNotNull(model, nameof(model));

            return await _client.PostAsync<SubscribeResponse>("/api/lists/subscribe", model).ConfigureAwait(false);
        }

        public async Task<ApiResponse<UnsubscribeResponse>> UnsubscribeAsync(UnsubscribeRequest model)
        {
            Ensure.ArgumentNotNull(model, nameof(model));

            return await _client.PostAsync<UnsubscribeResponse>("/api/lists/unsubscribe", model).ConfigureAwait(false);
        }
    }
}