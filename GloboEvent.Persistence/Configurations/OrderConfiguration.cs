using GloboEvent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.OrderTotal)
                .IsRequired();

            builder.Property(e => e.OrderPaid)
                .IsRequired();

            builder.Property(e => e.OrderPlaced)
                .IsRequired();
        }
    }
}
