using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using BookingForHumanService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Infrastructure.UnitOfWorks
{
    public  class UnitofWork :IUnitOfWork
    {
        private readonly BookingDbContext _context;
        public UnitofWork(BookingDbContext context)
        {
            _context = context;
            Customers =new CustomerRepository(_context);
            Providers = new ProviderRepository(_context);
            Bookings = new BookingRepository(_context);
            Reviews = new ReviewsRepository(_context);
            Addresses = new AddressRepository(_context);
            Notifications = new NotificationRepository(_context);


        }
        public ICustomerRepository Customers { get; private set; }
        public IProviderRepository Providers { get; private set; }
        IBookingRepository Bookings { get;  }
        IReviewRepository Reviews { get; }
        IAddressRepository Addresses { get; }
        INotificationRepository Notifications { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();   
        }
        public void Dispose()
        {
            _context.Dispose(); 
        }
    }
}
