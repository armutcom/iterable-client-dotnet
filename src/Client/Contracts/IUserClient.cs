using System.Threading.Tasks;
using Armut.Iterable.Client.Models.UserModels;

namespace Armut.Iterable.Client.Contracts
{
    public interface IUserClient
    {
        Task DeleteByEmailAsync(string email);

        Task<UserModel> GetByEmailAsync(string email);
        
        Task BulkUpdateAsync(BulkUpadateUserModel model);
        
        Task<UserModel> GetByUserIdAsync(string userId);
        
        Task DeleteByUserIdAsync(string userId);
        
        Task DisableDeviceAsync(DisableDeviceModel model);
        
        Task RegisterBrowserTokenAsync(RegisterBrowserTokenModel model);
        
        Task RegisterDeviceTokenAsync(RegisterDeviceTokenModel model);
        
        Task UpdateAsync(UpdateUserModel model);
        
        Task UpdateEmailAsync(UpdateEmailModel model);
    }
}