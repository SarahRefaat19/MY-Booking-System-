using BookingForHumanService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Infrastructure.Configurations
{
    internal class ReviewConfigurations : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> review)
        {
            review.ToTable(t =>
            {
                t.HasCheckConstraint( // Database Validation
                    "CK_Review_Rating",
                    "[Rating] >= 0.0 AND [Rating] <= 5.0"); 
            });

            review.Property(r => r.Rating).IsRequired();

            review.HasIndex(r => r.BookingId).IsUnique(); // One Review For Each Booking 

            review.Property(r => r.Comment).HasMaxLength(1000);


            // 1 ->  M : Customer -> Reviews 
            review.HasOne<Customer>()
                  .WithMany(c => c.Reviews)
                  .HasForeignKey(r => r.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);


            // 1 ->  M : Provider -> Reviews 
            review.HasOne<Provider>()
                  .WithMany(c => c.Reviews)
                  .HasForeignKey(r => r.ProviderId)
                  .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
