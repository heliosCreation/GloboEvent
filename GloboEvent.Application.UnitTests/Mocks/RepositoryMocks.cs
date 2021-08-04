using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GloboEvent.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var concertGuid = Guid.Parse("775B30D1-47C0-43F0-973A-6CEDC2676D0D");
            var playGuid = Guid.Parse("5492ADCF-A3D3-40A9-81F6-6E3BB2410A25");
            var musicalGuid = Guid.Parse("84906398-C787-4E4B-AC3A-959315A9AFF3") ; 
            var conferenceGuid = Guid.Parse("01CFB147-D027-4C3E-9022-091B6100FC13");

            var categories = new List<Category>()
            {
                new Category
                {
                    Id = concertGuid,
                    Name = "Concerts"
                },
                new Category
                {
                    Id = playGuid,
                    Name = "Plays"
                },
                new Category
                {
                    Id = musicalGuid,
                    Name = "Music"
                },
                new Category
                {
                    Id = conferenceGuid,
                    Name = "Conference"
                }
            };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(categories);

            mockCategoryRepository.Setup( repo => repo.IsNameUnique(It.IsAny<string>())).ReturnsAsync(
                (string name) => 
                {
                    return categories.Any(c => c.Name == name) == false; 
                });

            mockCategoryRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>())).ReturnsAsync(
                (Category category) =>
                {
                    categories.Add(category);
                    return category;
                });

            return mockCategoryRepository;
        }
    }
}
