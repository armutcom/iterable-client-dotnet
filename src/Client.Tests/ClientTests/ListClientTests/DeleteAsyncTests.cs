using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.ListModels;
using Armut.Iterable.Client.Models.UserModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.ListClientTests
{
    public class DeleteAsyncTests : BaseTestClass
    {
        private readonly IListClient _listClient;

        public DeleteAsyncTests()
        {
            _listClient = new ListClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _listClient.DeleteAsync(0)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _listClient.DeleteAsync(-5)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Delete()
        {
            const int listId = 14;
            string path = $"/api/lists/{listId}";

            MockRestClient.Setup(m => m.DeleteAsync<DeleteListResponse>(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse<DeleteListResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new DeleteListResponse
                {
                    Code = "Success"
                }
            });

            ApiResponse<DeleteListResponse> response = await _listClient.DeleteAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal("Success", response.Model.Code);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.DeleteAsync<DeleteListResponse>(It.Is<string>(a => a == path)), Times.Once);
        }
    }
}