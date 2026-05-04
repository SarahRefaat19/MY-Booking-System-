using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

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





        public async Task<IReadOnlyList<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> AddAsync(Booking entity)
        {
            await _context.Bookings.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Booking> UpdateAsync(Booking entity)
        {
            _context.Bookings.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Booking> Delete(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return booking!;
        }
    }
}