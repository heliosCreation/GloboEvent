using FluentAssertions;
using GloboEvent.Api.IntegrationTest.Base;
using GloboEvent.Application.Features.Categories;
using GloboEvent.Application.Features.Categories.Commands.Create;
using GloboEvent.Application.Features.Categories.Queries.GetCategoryWithEvent;
using GloboEvent.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public async Task GetAll_WhenCreatedOne_ReturnsOneMore()
        {
            // //Arrange
            await AuthenticateAsync();
            await CreateCategoryAsync(new CreateCategoryCommand { Name = "test" });

            // Act
            var response = await TestClient.GetAsync("api/Category/all");
            var content = await response.Content.ReadAsAsync<ApiResponse<CategoryVm>>();
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.DataList.Should().NotBeEmpty();
            content.DataList.Count().Should().Be(5);
        }

        [Fact]
        public async Task GetByIdWithEvents_ReturnsCorrectCategory_AndCorrectEvents()
        {
            await AuthenticateAsync();
            List<string> expectedEvents = new List<string>() { "John Egbert Live", "The State of Affairs: Michael Live!", "Clash of the DJs", "Spanish guitar hits with Manuel" };

            var response = await TestClient.GetAsync("api/Category/B0788D2F-8003-43C1-92A4-EDC76A7C5DDE/Event/true");
            var content = await response.Content.ReadAsAsync<ApiResponse<CategoryWithEventsVm>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Data.Should().NotBeNull();
            content.Data.Name.Should().Be("Concerts");
            content.Data.Events.Count().Should().Be(4);
            var eventNames = content.Data.Events.Select(e => e.Name).ToList();
            expectedEvents.ForEach(ex => eventNames.Should().Contain(ex));
        }

        [Fact]
        public async Task GetByIdWithEvent_ReturnsOnlyTodayEvents_WhenAsked()
        {
            await AuthenticateAsync();

            var response = await TestClient.GetAsync("api/Category/B0788D2F-8003-43C1-92A4-EDC76A7C5DDE/Event/false");
            var content = await response.Content.ReadAsAsync<ApiResponse<CategoryWithEventsVm>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Data.Should().NotBeNull();
            content.Data.Events.Count().Should().Be(1);
            var eventDates = content.Data.Events.Select(e => e.Date).ToList();
            eventDates.ForEach(d => d.Should().NotBeAfter(DateTime.Today));
        }

        [Fact]
        public async Task GetById_WithWrongId_shouldReturn_NotFound()
        {
            await AuthenticateAsync();

            var response = await TestClient.GetAsync("api/Category/62787623-4C52-43FE-B0C9-B7044FB5929B/Event/true");
            var content = await response.Content.ReadAsAsync<ApiResponse<CategoryWithEventsVm>>();

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_WithWrongParameters_ShouldReturn_Badrequest()
        {
            await AuthenticateAsync();

            var response = await TestClient.GetAsync("api/Category/wrongData/Event/true");
            var content = await response.Content.ReadAsAsync<ApiResponse<CategoryWithEventsVm>>();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
