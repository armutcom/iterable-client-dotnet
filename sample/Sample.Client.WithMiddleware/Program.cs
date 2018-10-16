using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Models.UserModels;
using System.Threading.Tasks;

namespace Sample.Client.WithMiddleware
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            DependencyFactory.Instance.RegisterDependencies();
            IRestClient restClient = DependencyFactory.Instance.Resolve<IRestClient>();
            IUserClient client = DependencyFactory.Instance.Resolve<IUserClient>();

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
