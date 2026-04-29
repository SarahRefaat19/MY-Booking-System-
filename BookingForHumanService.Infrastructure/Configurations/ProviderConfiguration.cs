using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


using System.Text;
using BookingForHumanService.Domain.Entities;

namespace BookingForHumanService.Infrastructure.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {

        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(c => c.Id);
            builder.OwnsOne(c => c.Name, n =>
            {
                n.Property(p => p.Name)
                 .HasColumnName("Name")
                 .HasMaxLength(50)
                 .IsRequired();
            });
            builder.OwnsOne(c => c.Phone, n =>
            {
                n.Property(p => p.Value)
                 .HasColumnName("Phone")
                 .HasMaxLength(50)
                 .IsRequired();
            }); 
            builder.Property(c => c.Description).HasMaxLength(500).IsRequired();
            builder.Property(c => c.ExperienceYears).IsRequired();


            builder.OwnsOne(c => c.Email, Email =>
            {
                Email.Property(e => e.Value).HasColumnName("Email").HasMaxLength(200).IsRequired();

            });
            builder.HasMany(b => b.Bookings)
                   .WithOne(c => c.Provider)
                   .HasForeignKey(c => c.ProviderId);

            builder.HasMany(b => b.Bookings)
       .WithOne(p => p.Provider)
       .HasForeignKey(b => b.ProviderId)
       .OnDelete(DeleteBehavior.NoAction);
        }
    }
}