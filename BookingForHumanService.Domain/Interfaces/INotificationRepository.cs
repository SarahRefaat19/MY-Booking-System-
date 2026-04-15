using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Interfaces
{
    public  interface INotificationRepository: IGenericRepository<Notification>
    {

        Task <IEnumerable<Notification>> GetByUserIdsync(int UserId);
        Task MarkAsRead(int notificationId);
       


    }
}
