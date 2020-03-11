using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Armut.Iterable.Client.Models.CommerceModels
{
    public class CommerceItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public dynamic DataFields { get; set; }

        public string[] Categories { get; set; }

        public float Price { get; set; }

    }
}
