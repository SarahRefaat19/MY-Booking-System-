using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Interfaces
{
    public  interface INotificationRepository
    {

        Task <Notification> GetByIdAsync(int Id);
        Task AddAsync(Notification notification);

    }
}
