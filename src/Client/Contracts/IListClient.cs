using Armut.Iterable.Client.Models.ListModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Armut.Iterable.Client.Contracts
{
    public interface IListClient
    {
        Task<GetAllListResponse> GetAllListsAsync();
        
        Task<IEnumerable<string>> GetUsersAsync(int listId);

        Task<long> GetSizeAsync(int listId);

        Task<int> CreateAsync(string name);

        Task DeleteAsync(int listId);

        Task<SubscribeResponse> SubscribeAsync(SubscribeRequest model);

        Task<UnsubscribeResponse> UnsubscribeAsync(UnsubscribeRequest model);
    }
}