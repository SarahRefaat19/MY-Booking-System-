using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookingDbContext _context;

        public ReviewRepository(BookingDbContext context)
        {
            _context = context;
        }
        public async Task<Review> AddAsync(Review entity)
        {
            await _context.Reviews.AddAsync(entity);

            return entity;
        }

        public async Task<Review> Delete(int id)
        {
            var reviewToDelete = await _context.Reviews.FindAsync(id);

            if (reviewToDelete  == null)
                throw new Exception("Review not found");

            _context.Reviews.Remove(reviewToDelete);

            return reviewToDelete;
        }

        public async Task<IReadOnlyList<Review>> GetAllAsync()
            =>  await _context.Reviews
                .Include(r => r.Booking)
                .AsNoTracking()
                .ToListAsync();

        public async Task<double> GetAvgRatingForProviderAsync(int providerId)
        {
            var provider = await _context.Providers.FindAsync(providerId);
            if (provider == null) throw new Exception("Provider not found");
            return provider.Rating; // calculated with every review added 
        }

        public async Task<IEnumerable<Review>> GetByCustomerIdAsync(int customerId)
          => await _context.Reviews
                .Include(r => r.Booking)
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();

        public async Task<Review?> GetByIdAsync(int id)
            => await _context.Reviews
                .Include(r => r.Booking)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<IEnumerable<Review>> GetByProviderIdAsync(int providerId)
            => await _context.Reviews
                .Include(r => r.Booking)
                .Where(r => r.ProviderId == providerId)
                .ToListAsync();


        public async Task<Review> UpdateAsync(Review review)
        {
            var reviewToUpdate = await _context.Reviews.FindAsync(review.Id);

            if (reviewToUpdate == null)
                throw new KeyNotFoundException();

            _context.Entry(reviewToUpdate).CurrentValues.SetValues(review);

            return reviewToUpdate;
        }
    }
}
