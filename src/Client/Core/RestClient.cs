using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Core;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task<ApiResponse> GetContentAsync(string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            HttpResponseMessage httpResponseMessage = await _client.GetAsync(path).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var apiResponse = new ApiResponse
            {
                HttpStatusCode = httpResponseMessage.StatusCode,
                Headers = httpResponseMessage.Content.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.First()),
                UrlPath = path,
                Content = content
            };

            return apiResponse;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string path) where T : class, new()
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            HttpResponseMessage httpResponseMessage = await _client.GetAsync(path).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var apiResponse = new ApiResponse<T>
            {
                HttpStatusCode = httpResponseMessage.StatusCode,
                Headers = httpResponseMessage.Content.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.First()),
                UrlPath = path
            };

            if (apiResponse.HttpStatusCode != HttpStatusCode.BadRequest
                && apiResponse.HttpStatusCode != HttpStatusCode.Unauthorized
                && !string.IsNullOrEmpty(content)
                && apiResponse.Headers["Content-Type"].Equals("application/json"))
            {
                apiResponse.Model = JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                apiResponse.Error = true;
                apiResponse.Content = content;
            }

            return apiResponse;
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string path, object request) where T : class, new()
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNull(request, nameof(request));

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Content = request.Serialize(),
                Method = HttpMethod.Post,
                RequestUri = new Uri(path, UriKind.RelativeOrAbsolute)
            };

            HttpResponseMessage httpResponseMessage = await _client.SendAsync(requestMessage).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var apiResponse = new ApiResponse<T>
            {
                HttpStatusCode = httpResponseMessage.StatusCode,
                Headers = httpResponseMessage.Content.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.First()),
                UrlPath = path,
                Model = JsonConvert.DeserializeObject<T>(content)
            };

            return apiResponse;
        }

        public async Task<ApiResponse<T>> DeleteAsync<T>(string path) where T : class, new()
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(path, UriKind.RelativeOrAbsolute)
            };

            HttpResponseMessage httpResponseMessage = await _client.SendAsync(requestMessage).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var apiResponse = new ApiResponse<T>
            {
                HttpStatusCode = httpResponseMessage.StatusCode,
                Headers = httpResponseMessage.Content.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.First()),
                UrlPath = path,
                Model = JsonConvert.DeserializeObject<T>(content)
            };

            return apiResponse;
        }

        public async Task<ApiResponse<T>> DeleteAsync<T>(string path, object request) where T : class, new()
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNull(request, nameof(request));

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Content = request.Serialize(),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(path, UriKind.RelativeOrAbsolute)
            };

            HttpResponseMessage httpResponseMessage = await _client.SendAsync(requestMessage).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var apiResponse = new ApiResponse<T>
            {
                HttpStatusCode = httpResponseMessage.StatusCode,
                Headers = httpResponseMessage.Content.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.First()),
                UrlPath = path,
                Model = JsonConvert.DeserializeObject<T>(content)
            };

            return apiResponse;
        }
    }
}