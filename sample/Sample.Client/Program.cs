using System;
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
            IListClient client = DependencyFactory.Instance.Resolve<IListClient>();
            
            var response = await client.GetUsersAsync(93798);


            Console.ReadLine();
        }
    }
}
