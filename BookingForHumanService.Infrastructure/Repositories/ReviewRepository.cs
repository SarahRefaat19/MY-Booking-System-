using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(BookingDbContext context) : base(context) { }
     

        public async Task<IEnumerable<Review>> GetByCustomerIdAsync(int customerId)
          => await _context.Reviews
                .Include(r => r.Booking)
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();

        public async Task<IEnumerable<Review>> GetByProviderIdAsync(int providerId)
            => await _context.Reviews
                .Include(r => r.Booking)
                .Where(r => r.ProviderId == providerId)
                .ToListAsync();
    }
}
