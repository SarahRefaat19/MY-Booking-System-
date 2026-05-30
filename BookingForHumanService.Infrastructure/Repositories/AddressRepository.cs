using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit.Sdk;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public  class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(BookingDbContext context) : base(context) { }

        public async Task<IEnumerable<Address>> GetByCustomerIdAsync(int Id)
        {
            return await _context.Addresses
                .Where(a => a.CustomerId == Id)
                .ToListAsync();
        }
    }
}
