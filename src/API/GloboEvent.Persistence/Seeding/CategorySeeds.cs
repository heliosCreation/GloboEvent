using GloboEvent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Persistence.Seeding
{
    public static class CategorySeeds
    {
        public static void Seed(ModelBuilder modelBuilder, Guid concertGuid, Guid musicalGuid, Guid playGuid, Guid conferenceGuid)
        {
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = concertGuid,
                Name = "Concerts"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = musicalGuid,
                Name = "Musicals"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = playGuid,
                Name = "Plays"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = conferenceGuid,
                Name = "Conferences"
            });
        }
    }
}
