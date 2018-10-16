using System.Threading.Tasks;
using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Models.UserModels;

namespace Sample.Client.DependencyInjection
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            DependencyFactory.Instance.RegisterDependencies();
            IRestClient restClient = DependencyFactory.Instance.Resolve<IRestClient>();
            IUserClient client = DependencyFactory.Instance.Resolve<IUserClient>();
            
            var result = await client.GetByEmailAsync("aksel@armut.com");

            await client.UpdateAsync(new UpdateUserRequest
            {
                Email = "aksel@armut.com",
                UserId = "aksel@armut.com",
                DataFields = new
                {
                    Name = "aksel"
                }
            });

            await client.DeleteByEmailAsync("aksel@armut.com");
        }
    }
}
