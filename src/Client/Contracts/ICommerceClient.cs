using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.CommerceModels;
using Armut.Iterable.Client.Models.EventModels;

namespace Armut.Iterable.Client.Contracts
{
    public interface ICommerceClient
    {
        Task<ApiResponse<TrackPurchaseResponse>> TrackPurchaseAsync(TrackPurchaseRequest model);
    }
}
