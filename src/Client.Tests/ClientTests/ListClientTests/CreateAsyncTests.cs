using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.ListModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.ListClientTests
{
    public class CreateAsyncTests : BaseTestClass
    {
        private readonly IListClient _listClient;

        public CreateAsyncTests()
        {
            _listClient = new ListClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _listClient.CreateAsync(null)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _listClient.CreateAsync(string.Empty)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Create_List()
        {
            const string path = "/api/lists";
            const string name = "test_list";
            const int listId = 14;

            MockRestClient.Setup(m => m.PostAsync<CreateListResponse>(It.Is<string>(a => a == path), It.Is<string>(a => a == name))).ReturnsAsync(new ApiResponse<CreateListResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new CreateListResponse
                {
                    ListId = listId
                }
            });

            ApiResponse<CreateListResponse> response = await _listClient.CreateAsync(name).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal(listId, response.Model.ListId);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.PostAsync<CreateListResponse>(It.Is<string>(a => a == path), It.Is<string>(a => a == name)), Times.Once);
        }
    }
}