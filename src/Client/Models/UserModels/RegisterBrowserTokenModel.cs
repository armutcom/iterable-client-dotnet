using Armut.Iterable.Client.Models.Base;

namespace Armut.Iterable.Client.Models.UserModels
{
    public class RegisterBrowserTokenModel : BaseUserModel
    {
        public string BrowserToken { get; set; }
    }
}