using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.RestClientTests
{
    public class GetContentAsyncTests : BaseTestClass
    {
        [Fact]
        public async Task Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            IRestClient _restClient = CreateRestClient();

            await Assert.ThrowsAsync<ArgumentNullException>(() => _restClient.GetContentAsync(null)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _restClient.GetContentAsync(string.Empty)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Return_ApiResponse_With_Content()
        {
            const string path = "/api/users/disableDevice";
            const string content = "test content";

            IRestClient _restClient = CreateRestClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = new StringContent(content)
            });

            ApiResponse apiResponse = await _restClient.GetContentAsync(path).ConfigureAwait(false);

            Assert.NotNull(apiResponse);
            Assert.Equal(HttpStatusCode.Created, apiResponse.HttpStatusCode);
            Assert.NotNull(apiResponse.Headers);
            Assert.Equal(1, apiResponse.Headers.Count);
            Assert.Equal(path, apiResponse.UrlPath);
            Assert.Equal(content, apiResponse.Content);

            VerifyRestClient(Times.Once(), HttpMethod.Get, path);
        }
    }
}