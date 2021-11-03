using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using GloboEvent.Test.Utilities.Mock;
using Moq;
using System.Linq;

namespace GloboEvent.Application.UnitTests.Mocks
{
    public class MockCategoryRepository : MockBaseExtension<Category, ICategoryRepository>
    {
        public override Mock<ICategoryRepository> GetEntityRepository()
        {
            Entities[0].Name = "Musicals";
            Entities[1].Name = "Plays";

            MockRepo.Setup(r => r.IsNameUnique(It.IsAny<string>())).ReturnsAsync((string name) =>
            {
                return !Entities.Any(e => e.Name == name);
            });

            return base.GetEntityRepository();
        }
    }
}

