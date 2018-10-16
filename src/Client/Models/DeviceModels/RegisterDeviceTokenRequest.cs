using Armut.Iterable.Client.Models.Base;
using Newtonsoft.Json;

namespace Armut.Iterable.Client.Models.DeviceModels
{
    public class RegisterDeviceTokenRequest : BaseUserModel
    {
        [JsonProperty("device")]
        public DeviceModel Device { get; set; }
    }
}