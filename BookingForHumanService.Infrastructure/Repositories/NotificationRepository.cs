using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using BookingForHumanService.Infrastructure.Data;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public  class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(BookingDbContext context) : base(context){ }
    }
}
