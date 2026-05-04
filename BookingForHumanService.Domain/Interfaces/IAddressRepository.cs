using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Interfaces
{
    public  interface IAddressRepository :IGenericRepository<Address>
    {
        Task<IEnumerable<Address>> GetByUserIdAsync(int Id);

    }
}
