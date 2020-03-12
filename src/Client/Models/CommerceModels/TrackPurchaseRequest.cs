using System.Collections.Generic;
using System.Dynamic;
using Armut.Iterable.Client.Models.UserModels;
using Newtonsoft.Json;

namespace Armut.Iterable.Client.Models.CommerceModels
{
    public class TrackPurchaseRequest
    {
        public TrackPurchaseRequest()
        {
            this.User = new UpdateUserRequest();
        }

        public string Id { get; set; }

        public UpdateUserRequest User { get; set; }

        public List<CommerceItem> Items { get; set; }

        [JsonProperty("campaignId")]
        public int CampaignId { get; set; }

        [JsonProperty("templateId")]
        public int TemplateId { get; set; }

        [JsonProperty("createdAt")]
        public int CreatedAt { get; set; }

        [JsonProperty("dataFields")]
        public dynamic DataFields { get; set; } = new ExpandoObject();

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
