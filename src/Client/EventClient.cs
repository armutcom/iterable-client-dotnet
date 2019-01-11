using System.Threading.Tasks;
using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.EventModels;

namespace Armut.Iterable.Client
{
    public class EventClient : IEventClient
    {
        private readonly IRestClient _client;

        public EventClient(IRestClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse<EventTrackResponse>> TrackAsync(EventTrackRequest model)
        {
            Ensure.ArgumentNotNull(model, nameof(model));

            return await _client.PostAsync<EventTrackResponse>("/api/events/track", model).ConfigureAwait(false);
        }
    }
}