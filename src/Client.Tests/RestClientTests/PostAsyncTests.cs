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
    public class PostAsyncTests : BaseTestClass
    {
        [Fact]
        public async Task Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            IRestClient _restClient = CreateRestClient();

            await Assert.ThrowsAsync<ArgumentNullException>(() => _restClient.PostAsync<UpdateUserResponse>(null, null)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _restClient.PostAsync<UpdateUserResponse>(string.Empty, null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Request_Is_Null()
        {
            const string path = "/api/users/update";
            IRestClient _restClient = CreateRestClient();

            await Assert.ThrowsAsync<ArgumentNullException>(() => _restClient.PostAsync<UpdateUserResponse>(path, null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Return_UpdateUserResponse()
        {
            const string path = "/api/users/update";
            const string content = "{\"msg\": \"test message\",\"code\": \"Success\"}";

            UpdateUserRequest updateUserRequest = new UpdateUserRequest
            {
                Email = "info@armut.com",
                UserId = "info@armut.com"
            };

            IRestClient _restClient = CreateRestClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            });

            ApiResponse<UpdateUserResponse> apiResponse = await _restClient.PostAsync<UpdateUserResponse>(path, updateUserRequest).ConfigureAwait(false);

            Assert.NotNull(apiResponse);
            Assert.Equal(HttpStatusCode.OK, apiResponse.HttpStatusCode);
            Assert.Equal(path, apiResponse.UrlPath);
            Assert.NotNull(apiResponse.Headers);
            Assert.Equal(1, apiResponse.Headers.Count);
            Assert.NotNull(apiResponse.Model);
            Assert.IsType<UpdateUserResponse>(apiResponse.Model);
            Assert.Equal("test message", apiResponse.Model.Msg);
            Assert.Equal("Success", apiResponse.Model.Code);

            VerifyRestClient(Times.Once(), HttpMethod.Post, path);
        }
    }
}