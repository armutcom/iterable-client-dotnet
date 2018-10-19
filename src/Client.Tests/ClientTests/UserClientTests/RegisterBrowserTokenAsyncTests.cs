using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.BrowserModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.UserClientTests
{
    public class RegisterBrowserTokenAsyncTests : BaseTestClass
    {
        private readonly IUserClient _userClient;

        public RegisterBrowserTokenAsyncTests()
        {
            _userClient = new UserClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Request_Is_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userClient.RegisterBrowserTokenAsync(null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Register_Browser()
        {
            const string path = "/api/users/registerBrowserToken";
            var request = new RegisterBrowserTokenRequest
            {
                Email = "info@armut.com",
                UserId = "info@armut.com"
            };

            MockRestClient.Setup(m => m.PostAsync<RegisterBrowserTokenResponse>(It.Is<string>(a => a == path), It.IsAny<RegisterBrowserTokenRequest>())).ReturnsAsync(new ApiResponse<RegisterBrowserTokenResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new RegisterBrowserTokenResponse
                {
                    Code = "Success"
                }
            });

            ApiResponse<RegisterBrowserTokenResponse> response = await _userClient.RegisterBrowserTokenAsync(request).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal("Success", response.Model.Code);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.PostAsync<RegisterBrowserTokenResponse>(It.Is<string>(a => a == path), It.IsAny<RegisterBrowserTokenRequest>()), Times.Once);
        }
    }
}