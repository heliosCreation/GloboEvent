using GloboEvent.Domain.Entities;
using GloboEvent.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Api.IntegrationTest.Base
{
    public static class Utilies
    {
        public static void InitializeDatabaseForTest(GloboEventDbContext dbContext)
        {
            var concertGuid = Guid.Parse("775B30D1-47C0-43F0-973A-6CEDC2676D0D");
            var playGuid = Guid.Parse("5492ADCF-A3D3-40A9-81F6-6E3BB2410A25");
            var musicalGuid = Guid.Parse("84906398-C787-4E4B-AC3A-959315A9AFF3");
            var conferenceGuid = Guid.Parse("01CFB147-D027-4C3E-9022-091B6100FC13");

            dbContext.Categories.Add(new Category
            {
                Id = concertGuid,
                Name = "Concerts"
            });
            dbContext.Categories.Add(new Category
            {
                Id = playGuid,
                Name = "Plays"
            }); dbContext.Categories.Add(new Category
            {
                Id = musicalGuid,
                Name = "Musicals"
            }); dbContext.Categories.Add(new Category
            {
                Id = conferenceGuid,
                Name = "Conference"
            });

            dbContext.SaveChanges();
        }
    }
}
