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
    public class DeleteByEmailAsyncTests : BaseTestClass
    {
        private readonly IUserClient _userClient;

        public DeleteByEmailAsyncTests()
        {
            _userClient = new UserClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userClient.DeleteByEmailAsync(null)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _userClient.DeleteByEmailAsync(string.Empty)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Delete_User()
        {
            const string email = "info@armut.com";
            string path = $"/api/users/{email}";

            MockRestClient.Setup(m => m.DeleteAsync<DeleteUserResponse>(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse<DeleteUserResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new DeleteUserResponse
                {
                    Code = "Success"
                }
            });

            ApiResponse<DeleteUserResponse> response = await _userClient.DeleteByEmailAsync(email).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal("Success", response.Model.Code);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.DeleteAsync<DeleteUserResponse>(It.Is<string>(a => a == path)), Times.Once);
        }
    }
}