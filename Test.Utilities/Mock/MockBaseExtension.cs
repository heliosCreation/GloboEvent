using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.Utilities.DataSet;

namespace GloboEvent.Test.Utilities.Mock
{
    using static CategorySet;
    public class MockBaseExtension<T, I>
        where T : AuditableEntity, new()
        where I : class, IAsyncRepository<T>
    {


        public List<T> Entities { get; set; } = new List<T>();

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
