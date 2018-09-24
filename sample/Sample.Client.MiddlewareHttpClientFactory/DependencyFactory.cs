using Armut.Iterable.Client;
using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Client.MiddlewareHttpClientFactory
{
    public class DependencyFactory
    {
        public static readonly DependencyFactory Instance = new DependencyFactory();
        private ServiceProvider _serviceProvider;

        private DependencyFactory() { }

        public void RegisterDependencies()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddIterableClient("your_api_key");

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T Resolve<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}