using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.DeviceModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.UserClientTests
{
    public class RegisterDeviceTokenAsyncTests : BaseTestClass
    {
        private readonly IUserClient _userClient;

        public RegisterDeviceTokenAsyncTests()
        {
            _userClient = new UserClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userClient.RegisterDeviceTokenAsync(null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Register_Device()
        {
            const string path = "/api/users/registerDeviceToken";
            var request = new RegisterDeviceTokenRequest
            {
                Email = "info@armut.com",
                UserId = "info@armut.com"
            };

            MockRestClient.Setup(m => m.PostAsync<RegisterDeviceTokenResponse>(It.Is<string>(a => a == path), It.IsAny<RegisterDeviceTokenRequest>())).ReturnsAsync(new ApiResponse<RegisterDeviceTokenResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new RegisterDeviceTokenResponse
                {
                    Code = "Success"
                }
            });

            ApiResponse<RegisterDeviceTokenResponse> response = await _userClient.RegisterDeviceTokenAsync(request).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal("Success", response.Model.Code);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.PostAsync<RegisterDeviceTokenResponse>(It.Is<string>(a => a == path), It.IsAny<RegisterDeviceTokenRequest>()), Times.Once);
        }
    }
}