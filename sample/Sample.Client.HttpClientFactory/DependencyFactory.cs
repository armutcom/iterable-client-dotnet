using Armut.Iterable.Client;
using Armut.Iterable.Client.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Sample.Client.HttpClientFactory
{
    public class DependencyFactory
    {
        public static readonly DependencyFactory Instance = new DependencyFactory();
        private ServiceProvider _serviceProvider;

        private DependencyFactory() { }

        public void RegisterDependencies()
        {
            var serviceCollection = new ServiceCollection();


            serviceCollection.AddHttpClient<IRestClient, RestClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.iterable.com/");
                client.DefaultRequestHeaders.Add("Api-Key", "your_api_key");
            });

            serviceCollection.AddTransient<IUserClient, UserClient>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T Resolve<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}