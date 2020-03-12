using System.Threading.Tasks;
using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.CommerceModels;

namespace Armut.Iterable.Client
{
    public class CommerceClient : ICommerceClient
    {
        private readonly IRestClient _restClient;

        public CommerceClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<ApiResponse<TrackPurchaseResponse>> TrackPurchaseAsync(TrackPurchaseRequest model)
        {
            Ensure.ArgumentNotNull(model, nameof(model));

            return await _restClient.PostAsync<TrackPurchaseResponse>("/api/commerce/trackPurchase", model).ConfigureAwait(false);
        }
    }
}
