using FluentAssertions;
using GloboEvent.Api.IntegrationTest.Base;
using GloboEvent.Application.Features.Categories;
using GloboEvent.Application.Features.Categories.Commands.Delete;
using GloboEvent.Application.Features.Categories.Commands.Update;
using GloboEvent.Application.Features.Categories.Queries.GetCategoryWithEvent;
using GloboEvent.Application.Responses;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GloboEvent.Api.IntegrationTest.Controllers.Category.Command
{
    using static Utils.CategoryTools;

    public class CategoryController_CommandsTest : IntegrationTestBase
    {
        #region Post
        [Fact]
        public async Task AddCategory_WhenValid_returnCorrectDataAndStatusCode()
        {
            await AuthenticateAsync();
            var categoryName = "Test";

            var response = await TestClient.PostAsJsonAsync("/api/Category/addCategory", new CreateCategoryCommand { Name = categoryName });
            var content = await response.Content.ReadAsAsync<ApiResponse<CategoryVm>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Data.Name.Should().Be(categoryName);
        }

        [Theory]
        [ClassData(typeof(CreateCategoryInvalidCommand))]
        public async Task AddCategory_WithInvalidData_returnBadRequest(string data)
        {
            await AuthenticateAsync();

            var response = await TestClient.PostAsJsonAsync("/api/Category/addCategory", new CreateCategoryCommand { Name = data });

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion

        #region Put
        [Fact]
        public async Task UpdateCategory_WithValidData_ReturnsOkStatusCode_AndUpdatesEntity()
        {
            await AuthenticateAsync();
            var updatedName = "Updated";
            var entityId = "B0788D2F-8003-43C1-92A4-EDC76A7C5DDE";

            var response = await TestClient.PutAsJsonAsync($"/api/Category/{Guid.Parse(entityId)}", new UpdateCategoryCommand {Id = Guid.Parse(entityId), Name = updatedName });
            var Getresponse = await TestClient.GetAsync($"api/Category/{entityId}/Event/true");
            var content = await Getresponse.Content.ReadAsAsync<ApiResponse<CategoryWithEventsVm>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Data.Name.Should().Be(updatedName);
        }
        [Theory]
        [ClassData(typeof(UpdateCategoryInvalidCommand))]
        public async Task UpdateCategory_WithInValidData_ReturnsBadRequest(string id, string data)
        {
            await AuthenticateAsync();


            var response = await TestClient.PutAsJsonAsync($"/api/Category/{Guid.Parse(id)}", new UpdateCategoryCommand { Id = Guid.Parse(id), Name = data });
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateCategory_NonRegisteredId_ReturnsNotFound()
        {
            await AuthenticateAsync();
            var entityId = "62787623-4C52-43FE-B0C9-B7044FB5929B";

            var response = await TestClient.PutAsJsonAsync("/api/Category", new UpdateCategoryCommand { Id = Guid.Parse(entityId), Name = "Update" });
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        #endregion

        #region Delete
        [Theory]
        [ClassData(typeof(DeleteCategoryCommandData))]
        public async Task Should_returnsValidStatusCode_DependingOnFoundOrNot(string id, int result)
        {
            await AuthenticateAsync();

            var response = await TestClient.DeleteAsync($"/api/Category/{Guid.Parse(id)}");

            ((int)response.StatusCode).Should().Be(result);

        }

        [Fact]
        public async Task Should_returnsNotFound_afterDeletingCategory()
        {
            await AuthenticateAsync();
            var entityId = "B0788D2F-8003-43C1-92A4-EDC76A7C5DDE";

            await TestClient.DeleteAsync($"/api/Category/{Guid.Parse(entityId)}");
            var response = await TestClient.GetAsync($"api/Category/{Guid.Parse(entityId)}/Event/true");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
        #endregion
    }
}
