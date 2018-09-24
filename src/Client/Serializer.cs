using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;

namespace Armut.Iterable.Client
{
    internal static class Serializer
    {     
        internal static HttpContent Serialize(this object @object)
        {
            var content = JsonConvert.SerializeObject(@object, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return new StringContent(content, Encoding.Unicode, "application/json");
        }
    }
}