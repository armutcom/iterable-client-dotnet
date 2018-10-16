using Armut.Iterable.Client.Models.Base;

namespace Armut.Iterable.Client.Models.DeviceModels
{
    public class DisableDeviceRequest : BaseUserModel
    {
        public string Token { get; set; }
    }
}