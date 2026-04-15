using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Infrastructure.Data
{
    public class BookingDbContext : IdentityDbContext
    {
        public BookingDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Address> Addresses { get; set; }

        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);
        }
    }
}
