using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.ListModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.ListClientTests
{
    public class GetAllListsAsyncTests : BaseTestClass
    {
        private readonly IListClient _listClient;

        public GetAllListsAsyncTests()
        {
            _listClient = new ListClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Retrieve_All_Lists()
        {
            const string path = "/api/lists";
            const int listId = 14;
            const string listName = "test list";
            DateTime date = DateTime.Now.AddDays(-10);

            MockRestClient.Setup(m => m.GetAsync<GetAllListResponse>(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse<GetAllListResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new GetAllListResponse
                {
                    Lists = new List<GetAllListModel>
                    {
                        new GetAllListModel
                        {
                            Id = listId,
                            CreatedAt = date.ToString(),
                            Name = listName
                        }
                    }
                }
            });

            ApiResponse<GetAllListResponse> response = await _listClient.GetAllListsAsync().ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.NotNull(response.Model.Lists);
            Assert.NotNull(response.Model.Lists.First());
            Assert.Equal(listName, response.Model.Lists.First().Name);
            Assert.Equal(listId, response.Model.Lists.First().Id);
            Assert.Equal(date.ToString(), response.Model.Lists.First().CreatedAt);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetAsync<GetAllListResponse>(It.Is<string>(a => a == path)), Times.Once);
        }
    }
}