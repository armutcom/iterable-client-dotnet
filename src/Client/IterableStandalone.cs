using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core;
using System;
using System.Net.Http;

namespace Armut.Iterable.Client
{
    public class IterableStandalone : IIterableFactory
    {
        private static HttpClient _client;
        private static RestClient _restClient;

        private IterableStandalone(string apiKey)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://api.iterable.com/")
            };
            _client.DefaultRequestHeaders.Add("Api-Key", apiKey);
            _restClient = new RestClient(_client);
        }

        public static IIterableFactory Create(string apiKey)
        {
            return new IterableStandalone(apiKey);
        }

        public UserClient CreateUserClient()
        {
            return new UserClient(_restClient);
        }
    }
}