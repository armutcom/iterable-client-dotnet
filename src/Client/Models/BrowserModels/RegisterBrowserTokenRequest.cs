using Armut.Iterable.Client.Models.Base;

namespace Armut.Iterable.Client.Models.BrowserModels
{
    public class RegisterBrowserTokenRequest : BaseUserModel
    {
        public string BrowserToken { get; set; }
    }
}