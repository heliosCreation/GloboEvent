using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using GloboEvent.Test.Utilities.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.Utilities.DataSet;

namespace GloboEvent.Application.UnitTests.Mocks
{
    using static CategorySet;
    public class MockCategoryRepository : MockBaseExtension<Category, ICategoryRepository>
    {
        public override Mock<ICategoryRepository> GetEntityRepository()
        {
            setCategoriesData();

            MockRepo.Setup(r => r.IsNameUnique(It.IsAny<string>())).ReturnsAsync((string name) =>
            {
                return !Entities.Any(e => e.Name == name);
            });

            return base.GetEntityRepository();
        }

        private void setCategoriesData()
        {
            Entities = new List<Category>()
            {

                new Category
                {
                    Id = CategoryId1, Name = CategoryName1,
                    Events = new List<Event>()
                    {
                        new Event { Date = DateTime.Today, CategoryId = CategoryId1 },
                        new Event { Date = DateTime.Today.AddDays(1), CategoryId = CategoryId1 }
                    }
                },

                new Category
                {
                    Id = CategoryId1, Name = CategoryName1,
                    Events = new List<Event>()
                    {
                        new Event { Date = DateTime.Today, CategoryId = CategoryId1 },
                        new Event { Date = DateTime.Today.AddDays(1), CategoryId = CategoryId1 }
                    }
                }
            };
        }
    }
}



