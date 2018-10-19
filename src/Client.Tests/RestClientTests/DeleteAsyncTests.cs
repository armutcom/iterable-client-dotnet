using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.UserModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.RestClientTests
{
    public class DeleteAsyncTests : BaseTestClass
    {
        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            IRestClient _restClient = CreateRestClient();

            await Assert.ThrowsAsync<ArgumentNullException>(() => _restClient.DeleteAsync<DeleteUserResponse>(null)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _restClient.DeleteAsync<DeleteUserResponse>(string.Empty)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Throw_ArgumentException_If_Request_Is_Null_Or_Empty()
        {
            const string path = "/api/users/delete";
            IRestClient _restClient = CreateRestClient();

            await Assert.ThrowsAsync<ArgumentNullException>(() => _restClient.DeleteAsync<DeleteUserResponse>(path, null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Return_DeleteUserResponse()
        {
            const string path = "/api/users/info@armut.com";
            const string content = "{\"msg\": \"test message\",\"code\": \"Success\"}";

            IRestClient _restClient = CreateRestClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            });

            ApiResponse<DeleteUserResponse> apiResponse = await _restClient.DeleteAsync<DeleteUserResponse>(path).ConfigureAwait(false);

            Assert.NotNull(apiResponse);
            Assert.Equal(HttpStatusCode.OK, apiResponse.HttpStatusCode);
            Assert.Equal(path, apiResponse.UrlPath);
            Assert.Equal(0, apiResponse.Headers.Count);
            Assert.NotNull(apiResponse.Model);
            Assert.IsType<DeleteUserResponse>(apiResponse.Model);
            Assert.Equal("test message", apiResponse.Model.Msg);
            Assert.Equal("Success", apiResponse.Model.Code);

            VerifyRestClient(Times.Once(), HttpMethod.Delete, path);
        }
    }
}
