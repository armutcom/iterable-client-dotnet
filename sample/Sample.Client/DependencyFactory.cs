using Armut.Iterable.Client;
using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Sample.Client.DependencyInjection
{
    public class DependencyFactory
    {
        public static readonly DependencyFactory Instance = new DependencyFactory();
        private ServiceProvider _serviceProvider;

        private DependencyFactory() { }

        public void RegisterDependencies()
        {
            var serviceCollection = new ServiceCollection();

            HttpClient iterableHttpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.iterable.com/")
            };

            iterableHttpClient.DefaultRequestHeaders.Add("Api-Key", "your_api_key");

            serviceCollection
                .AddSingleton<IRestClient, RestClient>()
                .AddTransient<IUserClient, UserClient>()
                .AddTransient<IListClient,ListClient>()
                //.AddSingleton(iterableHttpClient)
                .AddSingleton(clientFactory =>
                {
                    return (Func<string, HttpClient>)(key =>
                    {
                        switch (key)
                        {
                            case "IterableClient":
                                return iterableHttpClient;
                            default:
                                return null;
                        }
                    });
                });

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T Resolve<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}