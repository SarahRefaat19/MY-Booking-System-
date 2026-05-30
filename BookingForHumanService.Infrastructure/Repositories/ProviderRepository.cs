using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {

        public ProviderRepository(BookingDbContext context) : base(context) { }

        public async Task<Provider?> GetByEmailAsync(string email)
        {
            var provider = await _context.Providers.FindAsync(email);
            // This Is Logical and Function Wrong Use : Return Again 
            return provider;
        }
        public async Task<IEnumerable<Provider>> GetByServiceAsync(string serviceType)
        {
            return await _context.Providers
                  .Where(p => p.ServiceType.ToUpper().Contains(serviceType.ToUpper())) 
                  // This Is Performance Issue => Return Again 
                  .ToListAsync();
        }
        public async Task<IEnumerable<Provider>> GetByCityAsync(string city)
        {
            return await _context.Providers
                  .Where(p => p.City.ToUpper() == city.ToUpper())
                  .ToListAsync();
        }
    }
}
