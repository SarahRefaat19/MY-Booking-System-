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
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Phone).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(500).IsRequired();
            builder.Property(c => c.ExperienceYears).IsRequired();


            builder.OwnsOne(c => c.Email, Email =>
            {
                Email.Property(e => e.Value).HasColumnName("Email").HasMaxLength(200).IsRequired();

            });
            builder.HasMany(b => b.Bookings)
                   .WithOne(c => c.Provider)
                   .HasForeignKey(c => c.ProviderId);

            builder.HasIndex(p => p.Phone).IsUnique();

        }
    }
}