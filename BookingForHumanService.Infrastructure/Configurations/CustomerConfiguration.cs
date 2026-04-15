using BookingForHumanService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c=>c.Phone).HasMaxLength(20).IsRequired();
            builder.OwnsOne(c => c.Email, Email =>
            {
                Email.Property(e => e.Value).HasColumnName("Email").HasMaxLength(200).IsRequired();

            });
            builder.HasMany(b=>b.Bookings).WithOne(c=>c.Customer).HasForeignKey(c=>c.Id);

            builder.HasIndex(p => p.Phone).IsUnique();

        }

    }
}
