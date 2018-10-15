using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Sample.NetCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserClient _client;

        public HomeController(IUserClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            await _client.UpdateAsync(new UpdateUserRequest
            {
                Email = "aksel@armut.com",
                UserId = "aksel@armut.com",
                DataFields = new
                {
                    Name = "aksel",
                    Company = "Armut"
                }
            });

            return View();
        }
    }
}