using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Features.Categories;
using GloboEvent.Application.Features.Categories.Queries.GetCategoriesList;
using GloboEvent.Application.Features.Categories.Queries.GetCategoryWithEvent;
using GloboEvent.Application.Profiles;
using GloboEvent.Application.Responses;
using GloboEvent.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UnitTest.Utilities.DataSet;
using Xunit;

namespace GloboEvent.Application.UnitTests.Categories.Queries
{
    using static CategorySet;

    public class CategoryQueryHandlersTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public CategoryQueryHandlersTest()
        {
            _mockCategoryRepository = new MockCategoryRepository().GetEntityRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoryList_shouldReturns_EntireCollection_AndCorrectCast()
        {
            var handler = new GetCategoriesListQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<ApiResponse<CategoryVm>>();

            result.DataList.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnDesiredEntity_AndCorrectCast_WhenValidIdProvided()
        {
            var handler = new GetCategoryWithEventsQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoryWithEventQuery() { IncludeHistory = true, Id = CategoryId1 }, CancellationToken.None);

            result.ShouldBeOfType<ApiResponse<CategoryWithEventsVm>>();

            result.Data.ShouldNotBeNull();
            result.Data.Name.ShouldBe(CategoryName1);
            result.Data.Events.Count.ShouldBe(2);
        }
        [Fact]
        public async Task GetCategoryById_ShouldReturnCategory_WithTodayEvents_WhenAskedTo()
        {
            var handler = new GetCategoryWithEventsQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoryWithEventQuery() { IncludeHistory = false, Id = CategoryId1 }, CancellationToken.None);

            result.ShouldBeOfType<ApiResponse<CategoryWithEventsVm>>();

            result.Data.ShouldNotBeNull();
            result.Data.Name.ShouldBe(CategoryName1);
            result.Data.Events.Count.ShouldBe(1);
            result.Data.Events.ToList().ForEach(e => e.Date.Date.ShouldBe(DateTime.Today.Date));
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnNotFound_WhenInvalidIdProvided()
        {
            var handler = new GetCategoryWithEventsQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoryWithEventQuery() { IncludeHistory = false, Id = Guid.NewGuid() }, CancellationToken.None);

            result.ShouldBeOfType<ApiResponse<CategoryWithEventsVm>>();

            result.Data.ShouldBeNull();
            result.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
        }
    }
}
