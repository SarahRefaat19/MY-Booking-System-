using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using BookingForHumanService.Infrastructure.Data;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public  class NotificationRepository : INotificationRepository
    {
        private readonly BookingDbContext _context;

        public NotificationRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync( Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }
        public async Task<Notification> GetByIdAsync(int id)
        {
          var notification=  await _context.Notifications.FindAsync(id);
            if (notification == null)
                throw new Exception("This Notification not Found");
            return notification;

        }
    }
}
