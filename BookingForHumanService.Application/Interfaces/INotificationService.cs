using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Interfaces
{
    public interface INotificationService
    {
        Task MarkAsReadAsync(int userNotificationId);
        Task<List<UserNotification>> GetUserNotifications(int userId);
        Task SendByTargetUserType( Notification notification,int speceficUserId);
    }
}
