using FluentAssertions;
using GloboEvent.Api.IntegrationTest.Base;
using GloboEvent.Application.Features.Categories;
using GloboEvent.Application.Features.Categories.Commands.Create;
using GloboEvent.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GloboEvent.Api.IntegrationTest.Controllers.Category.Query
{
    public class CategoryController_QueriesTest : IntegrationTestBase
    {
        [Fact]
        public async Task GetAllReturnsSeededResponse()
        {
            // //Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/Category/all");
            var content = await response.Content.ReadAsAsync<ApiResponse<CategoryVm>>();
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.DataList.Should().NotBeEmpty();
            content.DataList.Count().Should().Be(4);
        }

        [Fact]
        public async Task GetAllWhenCreatedReturnsMore()
        {
            // //Arrange
            await AuthenticateAsync();
            await CreateCategoryAsync(new CreateCategoryCommand {Name = "test" });

            // Act
            var response = await TestClient.GetAsync("api/Category/all");
            var content = await response.Content.ReadAsAsync<ApiResponse<CategoryVm>>();
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.DataList.Should().NotBeEmpty();
            content.DataList.Count().Should().Be(5);
        }
    }
}
