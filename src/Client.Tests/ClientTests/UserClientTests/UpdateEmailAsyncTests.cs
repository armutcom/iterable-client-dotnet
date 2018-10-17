using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.UserModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.UserClientTests
{
    public class UpdateEmailAsyncTests : BaseTestClass
    {
        private readonly IUserClient _userClient;

        public UpdateEmailAsyncTests()
        {
            _userClient = new UserClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Request_Is_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userClient.UpdateEmailAsync(null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Update_Email()
        {
            string path = "/api/users/updateEmail";

            var request = new UpdateEmailRequest
            {
                CurrentEmail = "info@armut.com",
                NewEmail = "info@armut.com"
            };

            MockRestClient.Setup(m => m.PostAsync<UpdateUserResponse>(It.Is<string>(a => a == path), It.IsAny<UpdateEmailRequest>())).ReturnsAsync(new ApiResponse<UpdateUserResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new UpdateUserResponse
                {
                    Code = "Success"
                }
            });

            ApiResponse<UpdateUserResponse> response = await _userClient.UpdateEmailAsync(request).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal("Success", response.Model.Code);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.PostAsync<UpdateUserResponse>(It.Is<string>(a => a == path), It.IsAny<UpdateEmailRequest>()), Times.Once);
        }
    }
}