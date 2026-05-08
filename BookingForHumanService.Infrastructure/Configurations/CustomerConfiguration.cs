using BookingForHumanService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace BookingForHumanService.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.HasKey(c => c.Id);
            builder.OwnsOne(c => c.Name, n =>
            {
                n.Property(p => p.Name)
                 .HasColumnName("Name")
                 .HasMaxLength(50)
                 .IsRequired();
            });
            builder.OwnsOne(c => c.Phone, p =>
            {
                p.Property(x => x.Value)
                 .HasColumnName("Phone")
                 .HasMaxLength(20)
                 .IsRequired();
            });

            builder.OwnsOne(c => c.Email, Email =>
            {
                Email.Property(e => e.Value).HasColumnName("Email").HasMaxLength(200).IsRequired();

            });
            builder.HasMany(c => c.Bookings)
                   .WithOne(b => b.Customer)
                   .HasForeignKey(b => b.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.User)
                   .WithOne()
                   .HasForeignKey<Customer>(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict);



        }

    }
}
