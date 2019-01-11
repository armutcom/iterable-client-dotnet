using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.UserModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.RestClientTests
{
    public class GetAsyncTests : BaseTestClass
    {
        [Fact]
        public async Task Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            IRestClient _restClient = CreateRestClient();

            await Assert.ThrowsAsync<ArgumentNullException>(() => _restClient.GetAsync<UpdateUserResponse>(null)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _restClient.GetAsync<UpdateUserResponse>(string.Empty)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Return_RetrieveUserResponse()
        {
            const string userId = "info@armut.com";
            string path = $"/api/users/byUserId/{userId}";

            string content = string.Empty;

            IRestClient _restClient = CreateRestClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(content)
            });

            ApiResponse<RetrieveUserResponse> apiResponse = await _restClient.GetAsync<RetrieveUserResponse>(path).ConfigureAwait(false);

            Assert.NotNull(apiResponse);
            Assert.Equal(HttpStatusCode.OK, apiResponse.HttpStatusCode);
            Assert.Equal(path, apiResponse.UrlPath);
            Assert.NotNull(apiResponse.Headers);
            Assert.Equal(2, apiResponse.Headers.Count);
            Assert.Null(apiResponse.Model);

            VerifyRestClient(Times.Once(), HttpMethod.Get, path);
        }
    }

    public class JsonContent : HttpContent
    {
        private readonly MemoryStream _stream = new MemoryStream();

        public JsonContent(string content)
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var jw = new JsonTextWriter(new StreamWriter(_stream));
            var serializer = new JsonSerializer();
            serializer.Serialize(jw, content);
            jw.Flush();
            _stream.Position = 0;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return _stream.CopyToAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _stream.Length;
            return true;
        }
    }
}