using System;
using System.Net.Http;
using System.Threading.Tasks;
using Armut.Iterable.Client.Contracts;
using Newtonsoft.Json;

namespace Armut.Iterable.Client.Core
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

        public async Task<T> GetAsync<T>(string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            HttpResponseMessage httpResponseMessage = await _client.GetAsync(path).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(content))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> PostAsync<T>(string path, object request)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Content = request.Serialize(),
                Method = HttpMethod.Post,
                RequestUri = new Uri(path, UriKind.RelativeOrAbsolute)
            };

            HttpResponseMessage httpResponseMessage = await _client.SendAsync(requestMessage).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(content))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> DeleteAsync<T>(string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(path, UriKind.RelativeOrAbsolute)
            };
            
            HttpResponseMessage httpResponseMessage = await _client.SendAsync(requestMessage).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(content))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> DeleteAsync<T>(string path, object request)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Content = request.Serialize(),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(path, UriKind.RelativeOrAbsolute)
            };

            HttpResponseMessage httpResponseMessage = await _client.SendAsync(requestMessage).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(content))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}