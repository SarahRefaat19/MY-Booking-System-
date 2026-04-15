using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookingForHumanService.Domain.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
       
        Task<Customer> GetByEmail(string email);
         Task<IReadOnlyList<Customer>> GetCustomersPagedAsync(int page, int pageSize);
        Task<int> GetTotalCustomersCountAsync();
        Task <Customer> SearchCustomerAsync(int Id);
    }
}
