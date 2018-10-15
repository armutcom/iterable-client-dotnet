using System.Threading.Tasks;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.BrowserModels;
using Armut.Iterable.Client.Models.DeviceModels;
using Armut.Iterable.Client.Models.UserModels;

namespace Armut.Iterable.Client.Contracts
{
    public interface IUserClient
    {
        Task<ApiResponse<DeleteUserResponse>> DeleteByEmailAsync(string email);

        Task<ApiResponse<RetrieveUserResponse>> GetByEmailAsync(string email);
        
        Task<ApiResponse<BulkUpdateUserResponse>> BulkUpdateAsync(BulkUpadateUserRequest model);
        
        Task<ApiResponse<RetrieveUserResponse>> GetByUserIdAsync(string userId);
        
        Task<ApiResponse<DeleteUserResponse>> DeleteByUserIdAsync(string userId);
        
        Task<ApiResponse<DisableDeviceResponse>> DisableDeviceAsync(DisableDeviceRequest model);
        
        Task<ApiResponse<RegisterBrowserTokenResponse>> RegisterBrowserTokenAsync(RegisterBrowserTokenRequest model);
        
        Task<ApiResponse<RegisterDeviceTokenResponse>> RegisterDeviceTokenAsync(RegisterDeviceTokenRequest model);
        
        Task<ApiResponse<UpdateUserResponse>> UpdateAsync(UpdateUserRequest model);
        
        Task<ApiResponse<UpdateUserResponse>> UpdateEmailAsync(UpdateEmailRequest model);
    }
}