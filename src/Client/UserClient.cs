using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.UserModels;
using System.Threading.Tasks;

namespace Armut.Iterable.Client
{
    public class UserClient : IUserClient
    {
        private readonly IRestClient _client;

        public UserClient(IRestClient client)
        {
            _client = client;
        }

        public async Task DeleteByEmailAsync(string email)
        {
            await _client.DeleteAsync<UserModel>($"/api/users/{email}").ConfigureAwait(false);
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            ApiResponse<RetrieveUserModel> response = await _client.GetAsync<RetrieveUserModel>($"/api/users/{email}").ConfigureAwait(false);
            return response?.Model.User;
        }

        public async Task BulkUpdateAsync(BulkUpadateUserModel model)
        {
            await _client.PostAsync<BulkUpdateUserResponse>("/api/users/bulkUpdate", model.Users).ConfigureAwait(false);
        }

        public async Task<UserModel> GetByUserIdAsync(string userId)
        {
            ApiResponse<RetrieveUserModel> user = await _client.GetAsync<RetrieveUserModel>($"/api/users/byUserId/{userId}").ConfigureAwait(false);
            return user?.Model.User;
        }

        public async Task DeleteByUserIdAsync(string userId)
        {
            await _client.DeleteAsync<UserModel>($"/api/users/byUserId/{userId}").ConfigureAwait(false);
        }

        public async Task DisableDeviceAsync(DisableDeviceModel model)
        {
            await _client.PostAsync<DisableDeviceResponse>("/api/users/disableDevice", model).ConfigureAwait(false);
        }

        public async Task RegisterBrowserTokenAsync(RegisterBrowserTokenModel model)
        {
            await _client.PostAsync<RegisterBrowserTokenResponse>("/api/users/registerBrowserToken", model).ConfigureAwait(false);
        }

        public async Task RegisterDeviceTokenAsync(RegisterDeviceTokenModel model)
        {
            await _client.PostAsync<RegisterDeviceResponse>("/api/users/registerDeviceToken", model).ConfigureAwait(false);
        }

        public async Task UpdateAsync(UpdateUserModel model)
        {
            await _client.PostAsync<UpdateUserReponse>("/api/users/update", model).ConfigureAwait(false);
        }

        public async Task UpdateEmailAsync(UpdateEmailModel model)
        {
            await _client.PostAsync<UpdateUserReponse>("/api/users/updateEmail", model).ConfigureAwait(false);
        }
    }
}