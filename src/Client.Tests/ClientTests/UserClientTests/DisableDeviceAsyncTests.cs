using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.DeviceModels;
using Armut.Iterable.Client.Models.UserModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.UserClientTests
{
    public class DisableDeviceAsyncTests : BaseTestClass
    {
        private readonly IUserClient _userClient;

        public DisableDeviceAsyncTests()
        {
            _userClient = new UserClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userClient.DisableDeviceAsync(null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Disable_Device()
        {
            const string path = "/api/users/disableDevice";
            var request = new DisableDeviceRequest
            {
                UserId = "info@armut.com",
                Email = "info@armut.com"
            };

            MockRestClient.Setup(m => m.PostAsync<DisableDeviceResponse>(It.Is<string>(a => a == path), It.IsAny<DisableDeviceRequest>())).ReturnsAsync(new ApiResponse<DisableDeviceResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new DisableDeviceResponse
                {
                    Code = "Success"
                }
            });

            ApiResponse<DisableDeviceResponse> response = await _userClient.DisableDeviceAsync(request).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal("Success", response.Model.Code);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.PostAsync<DisableDeviceResponse>(It.Is<string>(a => a == path), It.IsAny<DisableDeviceRequest>()), Times.Once);
        }
    }
}