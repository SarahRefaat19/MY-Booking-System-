using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public class ProviderRepository : IGenericRepository<Provider>, IProviderRepository
    {

        private readonly BookingDbContext _context;

        public ProviderRepository(BookingDbContext context)
        {
            _context = context;
        }


        public async  Task<IReadOnlyList<Provider>> GetAllAsync()
        {
            return await _context.Providers.ToListAsync();
        }
        public async Task<Provider> GetByIdAsync(int id)
        {
            var provider= await _context.Providers.FindAsync(id);
            if (provider == null)
            {
                throw new Exception("Cannot Found Provder ");
            }
            return provider;
        }
        public async Task<Provider> AddAsync(Provider provider)
        {
           await _context.Providers.AddAsync(provider);
            return provider;

        }
        public Task<Provider> UpdateAsync(Provider provider)
        {
            _context.Providers.Update(provider);
            return Task.FromResult(provider);
        }
        public async Task<Provider> Delete(int Id)
        {
            var provider = await _context.Providers.FindAsync(Id);
            if (provider == null) throw new KeyNotFoundException();
            _context.Providers.Remove(provider);
            return provider;
        }

        public async Task<Provider> GetByEmailAsync(string email)
        {
            var provider = await _context.Providers.FindAsync(email);
            if (provider == null)
            {
                throw new Exception("Cannot Found Provider ");
            }
            return provider;
        }
        public async Task<Provider> GetWithAverageRatingAsync(int providerId)
        {
            var provider = await _context.Providers.Include(r=>r.Reviews).FirstOrDefaultAsync(p=>p.Id ==providerId);
            if (provider == null)
            {
                throw new Exception("Cannot Found Provider ");
            }

            var avgRating = provider.Reviews.Any() ? provider.Reviews.Average(r => r.Rating): 0;

            return provider;
        }


        public async Task<IEnumerable<Provider>> GetByServiceAsync(string serviceType)
        {

            return await _context.Providers
                  .Where(p => p.ServiceType == serviceType)
                  .ToListAsync();
        }
        public async Task<IEnumerable<Provider>> GetByCityAsync(string city)
        {
            return await _context.Providers
                  .Where(p => p.City == city)
                  .ToListAsync();
        }


    }
}
