using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Interfaces
{
    public interface IReviewRepository: IGenericRepository<Review>
    {
        Task <IEnumerable<Review>> GetByProviderIdAsync(int providerId);
        Task<IEnumerable<Review>> GetByCustomerIdAsync( int customerId);
    }
}
