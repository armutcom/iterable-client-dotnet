using System;
using System.Net.Http;
using System.Threading.Tasks;
using Armut.Iterable.Client.Contracts;
using Newtonsoft.Json;

namespace Armut.Iterable.Client
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _client;

        public RestClient(Func<string, HttpClient> clientFactory)
        {
            _client = clientFactory("IterableClient");
        }

        public RestClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage httpResponseMessage = await _client.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task PostAsync<T>(string url, T request)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Content = request.Serialize(),
                Method = HttpMethod.Post,
                RequestUri = new Uri(url, UriKind.RelativeOrAbsolute)
            };

            var response = await _client.SendAsync(requestMessage).ConfigureAwait(false);
        }

        public async Task DeleteAsync(string url)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url, UriKind.RelativeOrAbsolute)
            };

            var response = await _client.SendAsync(requestMessage).ConfigureAwait(false);
        }

        public async Task DeleteAsync<T>(string url, T request)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Content = request.Serialize(),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url, UriKind.RelativeOrAbsolute)
            };

            var response = await _client.SendAsync(requestMessage).ConfigureAwait(false);
        }
    }
}