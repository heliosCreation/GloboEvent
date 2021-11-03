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
using System.Threading;
using System.Threading.Tasks;
using Test.Utilities.DataSet;
using Xunit;

namespace GloboEvent.Application.UnitTests.Categories.Queries
{
    using static CategorySet;

    public class CategoryQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public CategoryQueryHandlerTest()
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

            var result = await handler.Handle(new GetCategoryWithEventQuery() {IncludeHistory = true, Id = CategoryId1 }, CancellationToken.None);

            result.ShouldBeOfType<ApiResponse<CategoryWithEventsVm>>();

            result.Data.ShouldNotBeNull();
            result.Data.Events.Count.ShouldBe(2);
        }
    }
}
