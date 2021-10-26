﻿using GloboEvent.Api.IntegrationTest.Base;
using GloboEvent.API;
using GloboEvent.Application.Features.Categories;
using GloboEvent.Application.Responses;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace GloboEvent.Api.IntegrationTest.Controllers
{
    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CategoryControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task return_SuccessfullStatuscode()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/category/all");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<CategoryVm>>(responseString);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse<CategoryVm>>(result);
        }
    }
}
