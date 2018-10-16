using Armut.Iterable.Client.Contracts;
using System;
using System.Threading.Tasks;

namespace Sample.Client.HttpClientFactory
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            DependencyFactory.Instance.RegisterDependencies();
            IUserClient client = DependencyFactory.Instance.Resolve<IUserClient>();

            var result = await client.GetByEmailAsync("aksel@armut.com");

            Console.WriteLine(result?.Model?.User?.UserId);
        }
    }
}