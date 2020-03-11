using System.Collections.Generic;
using Armut.Iterable.Client.Models.UserModels;
using Newtonsoft.Json;

namespace Armut.Iterable.Client.Models.CommerceModels
{
    public class TrackPurchaseRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user")]
        public UpdateUserRequest User { get; set; }

        [JsonProperty("items")]
        public List<CommerceItem> Items { get; set; }

        [JsonProperty("campaignId")]
        public int CampaignId { get; set; }

        [JsonProperty("templateId")]
        public int TemplateId { get; set; }

        [JsonProperty("createdAt")]
        public int CreatedAt { get; set; }

        [JsonProperty("dataFields")]
        public dynamic DataFields { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
