using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace BookingForHumanService.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BookingDbContext context) : base(context) { }
     
        public async Task<IReadOnlyList<Customer>> GetCustomersPagedAsync(int pageNumber, int pageSize)
        {
            var customerpages = await _context.Customers.OrderBy(c => c.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return customerpages;
        }

        public async Task<int> GetTotalCustomersCountAsync()
        {
            return await _context.Customers.CountAsync();
        }
     
        public async Task<Customer?> GetByEmailAsync(string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email.Value == email);
            return customer;
        }

    }
}
