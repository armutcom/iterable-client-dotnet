using Armut.Iterable.Client;
using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Models.UserModels;
using System.Threading.Tasks;

namespace Sample.Client.Standalone
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            IIterableFactory iterableFactory = IterableStandalone.Create("your_api_key");
            UserClient client = iterableFactory.CreateUserClient();

            await client.UpdateAsync(new UpdateUserRequest
            {
                Email = "aksel@armut.com",
                UserId = "aksel@armut.com",
                DataFields = new
                {
                    Name = "aksel test"
                }
            });
        }
    }
}