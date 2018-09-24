using Armut.Iterable.Client.Models.Base;
using Newtonsoft.Json;

namespace Armut.Iterable.Client.Models.UserModels
{
    public class RegisterDeviceTokenModel : BaseUserModel
    {
        [JsonProperty("device")]
        public DeviceModel Device { get; set; }
    }
}