using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace BookingForHumanService.Infrastructure.Repositories
{
   public class CustomerRepository : IGenericRepository<Customer> , ICustomerRepository
    {

        private  readonly BookingDbContext _context;

        public CustomerRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync()
        {
           return  await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id )
        {
            var customer = await _context.Customers.FindAsync(id);

            if (  customer is null)
                throw new KeyNotFoundException($"Customer with Id {id} not found.");
            return customer;

        }
      public async Task<IReadOnlyList<Customer>>  GetCustomersPagedAsync(int pageNumber, int pageSize)
        {
           var customerpages=  await _context.Customers.OrderBy(c=>c.Id).Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();
            return customerpages;
        }

        public async Task<int> GetTotalCustomersCountAsync()
        {
            return await _context.Customers.CountAsync();
        }
        public async Task<Customer> AddAsync( Customer customer)
        {
             await _context.Customers.AddAsync(customer);
            return customer;
        }
        public  Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return Task.FromResult(customer);
        }
        public async Task<Customer> Delete(int id )
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) throw new KeyNotFoundException();
            _context.Customers.Remove(customer);
            return customer;
        }
        public async Task<Customer> GetByEmail(string email)
        {
 var customer= await _context.Customers.FirstOrDefaultAsync(c => c.Email.Value == email);
            if (customer is null)
                throw new ArgumentNullException($"Customer with Id {email} not found.");
            return customer;

        }

      public async  Task<Customer> SearchCustomerAsync(int Id)
        {
          var customer=  await _context.Customers.FindAsync(Id);
            if (customer is null)
            {
                throw new KeyNotFoundException("Customer Not Found");
            }
            return customer;
        }


    }
}
