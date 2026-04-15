using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Interfaces
{
    public  interface IUnitOfWork :IDisposable
    {


        ICustomerRepository Customers { get; }
        IBookingRepository Bookings { get; }
        IAddressRepository Addresses { get; }
        IProviderRepository Providers { get; }
        INotificationRepository Notifications { get; }
        IReviewRepository Reviews { get; }


        Task<int> SaveChangesAsync();

    }
}
