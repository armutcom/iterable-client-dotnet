using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.EventModels;
using System.Threading.Tasks;

namespace Armut.Iterable.Client.Contracts
{
    public interface IEventClient
    {
        Task<ApiResponse<EventTrackResponse>> TrackAsync(EventTrackRequest model);
    }
}