using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.UserModels;
using Armut.Iterable.Client.Tests.Base;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.RestClientTests
{
    public class GetAsyncTests : BaseTestClass
    {
        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            IRestClient _restClient = CreateRestClient();

            await Assert.ThrowsAsync<ArgumentNullException>(() => _restClient.GetAsync<UpdateUserResponse>(null)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _restClient.GetAsync<UpdateUserResponse>(string.Empty)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Return_UpdateUserResponse()
        {
            const string path = "/api/users/update";
            const string content = "{\"msg\": \"test message\",\"code\": \"Success\"}";

            IRestClient _restClient = CreateRestClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            });

            ApiResponse<UpdateUserResponse> apiResponse = await _restClient.GetAsync<UpdateUserResponse>(path).ConfigureAwait(false);

            Assert.NotNull(apiResponse);
            Assert.Equal(HttpStatusCode.OK, apiResponse.HttpStatusCode);
            Assert.Equal(path, apiResponse.UrlPath);
            Assert.Equal(0, apiResponse.Headers.Count);
            Assert.NotNull(apiResponse.Model);
            Assert.IsType<UpdateUserResponse>(apiResponse.Model);
            Assert.Equal("test message", apiResponse.Model.Msg);
            Assert.Equal("Success", apiResponse.Model.Code);
        }
    }
}