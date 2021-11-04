using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Features.Categories.Commands.Delete;
using GloboEvent.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Test.Utilities.DataSet.CategorySet;

namespace GloboEvent.Application.UnitTests.Categories.Commands
{
    public class DeleteCategoryTest
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly DeleteCategoryCommandHandler _handler;

        public DeleteCategoryTest()
        {
            _mockCategoryRepository = new MockCategoryRepository().GetEntityRepository();
            _handler = new DeleteCategoryCommandHandler(_mockCategoryRepository.Object);


        }


        [Theory]
        [ClassData(typeof(DeleteCategoryCommandData))]
        public async Task Handle_DeleteCategory_ShouldReturns_AppropriateValues(Guid id, int statusCode)
        {
            var command = new DeleteCategoryCommand(id);
            var result = await _handler.Handle(command, CancellationToken.None);

            result.StatusCode.ShouldBe(statusCode);
        }
    }
}
