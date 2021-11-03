using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GloboEvent.Test.Utilities.Mock
{
    public class MockBaseExtension<T, I>
        where T : AuditableEntity, new()
        where I : class, IAsyncRepository<T>
    {
        public static string Id1 { get; set; } = "B0788D2F-8003-43C1-92A4-EDC76A7C5DDE";
        public static string Id2 { get; set; } = "62787623-4C52-43FE-B0C9-B7044FB5929B";

        public List<T> Entities { get; set; } = new List<T>
        {
            new T{Id = Guid.Parse(Id1)},
            new T{Id = Guid.Parse(Id2)}
        };

        public Mock<I> MockRepo { get; set; } = new Mock<I>();

        public virtual Mock<I> GetEntityRepository()
        {

            MockRepo.Setup(r => r.ListAllAsync()).ReturnsAsync(Entities);

            MockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return Entities.Where(c => c.Id == id).FirstOrDefault();
            });

            MockRepo.Setup(r => r.AddAsync(It.IsAny<T>())).ReturnsAsync((T entity) =>
            {
                Entities.Add(entity);
                return entity;
            });

            MockRepo.Setup(r => r.UpdateAsync(It.IsAny<T>())).Callback((T entity) =>
            {
                var entityToUpdate = Entities.Where(c => c.Id == entity.Id).FirstOrDefault();
                entityToUpdate = entity;
            });

            MockRepo.Setup(r => r.DeleteAsync(It.IsAny<T>())).Callback((T entity) =>
            {
                var entityToDelete = Entities.Where(c => c.Id == entity.Id).FirstOrDefault();
                Entities.Remove(entityToDelete);
            });
            return MockRepo;
        }
    }
}
