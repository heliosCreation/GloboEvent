using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using Moq;

namespace GloboEvent.Application.UnitTests.Mocks
{
    public class MockCategoryRepository : MockBaseExtension<Category, ICategoryRepository>
    {
        public override Mock<ICategoryRepository> GetEntityRepository()
        {
            Entities[0].Name = "plays";
            Entities[1].Name = "Concerts";
            return base.GetEntityRepository();
        }
    }
}

