namespace Armut.Iterable.Client.Models.DeviceModels
{
    public class DeviceModel
    {
        public string Token { get; set; }
        
        public string Platform { get; set; }
        
        public string ApplicationName { get; set; }
        
        public dynamic DataFields { get; set; }
    }
}