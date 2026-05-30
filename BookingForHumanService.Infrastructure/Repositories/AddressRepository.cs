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
    public  class AddressRepository :IAddressRepository
    {
        public readonly BookingDbContext _Context;
        public AddressRepository(BookingDbContext context)
        {
            _Context = context;
        }
        public async Task<IReadOnlyList<Address>> GetAllAsync()
        {
            return await _Context.Addresses
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _Context.Addresses
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Address> AddAsync(Address address)
        {
            _Context.Addresses.Add(address);

            await _Context.SaveChangesAsync();
            return address;
        }
        public async Task<Address> UpdateAsync(Address address)
        {
            _Context.Addresses.Update(address);
            await _Context.SaveChangesAsync();
            return address;
        }
        public async Task<Address> Delete(int Id)
        {
            var address = await _Context.Addresses.FindAsync(Id);

            if (address == null)
            {
                 throw new Exception("Address not found ");
            }
            _Context.Addresses.Remove(address);
            await _Context.SaveChangesAsync();
            return address;
            
        }
        public async Task<IEnumerable<Address>> GetByUserIdAsync(int Id)
        {
            return await _Context.Addresses
                .Where(a => a.CustomerId == Id)
                .ToListAsync();
        }

    }
}
