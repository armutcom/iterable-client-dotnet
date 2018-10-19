using Armut.Iterable.Client.Contracts;
using System;
using System.Threading.Tasks;

namespace Sample.Client.DependencyInjection
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            DependencyFactory.Instance.RegisterDependencies();
            IListClient client = DependencyFactory.Instance.Resolve<IListClient>();

            var response = await client.GetSizeAsync(93798);

            Console.ReadLine();
        }
    }
}
