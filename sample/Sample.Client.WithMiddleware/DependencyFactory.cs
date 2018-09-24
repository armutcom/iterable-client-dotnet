using Armut.Iterable.Client.Extension;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Sample.Client.WithMiddleware
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

            serviceCollection.AddIterableClient();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T Resolve<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}