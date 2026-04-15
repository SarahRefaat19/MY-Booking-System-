using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Interfaces
{
    public interface  IProviderRepository :IGenericRepository<Provider>
    {

        Task<Provider> GetByEmailAsync(string email);
        Task<Provider> GetWithAverageRatingAsync(int providerId);


        Task<IEnumerable<Provider>> GetByServiceAsync(string serviceType);
        Task<IEnumerable<Provider>> GetByCityAsync(string city);

    }
}
