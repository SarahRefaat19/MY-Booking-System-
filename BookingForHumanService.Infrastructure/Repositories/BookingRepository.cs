using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>,IBookingRepository
    {
        public BookingRepository(BookingDbContext context) : base(context) { }
       
        public async Task<IReadOnlyList<Booking>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Bookings
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Booking>> GetByProviderIdAsync(int providerId)
        {
            return await _context.Bookings
                .Where(b => b.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Booking>> GetByStatusAsync(BookingStatus status)
        {
            return await _context.Bookings
                .Where(b => b.Status == status)
                .ToListAsync();
        }
    }
}