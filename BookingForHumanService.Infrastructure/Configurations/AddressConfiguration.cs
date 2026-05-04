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
    public class AddressConfiguration :IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Customer)
                  .WithMany(c => c.Addresses)
                  .HasForeignKey(b => b.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);



        }
    

    }
}
