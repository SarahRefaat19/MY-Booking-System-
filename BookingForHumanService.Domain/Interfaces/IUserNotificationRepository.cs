using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Domain.Interfaces
{
    public interface IUserNotificationRepository
    {
        Task AddRangeAsync(List<UserNotification> notifications);
        Task<List<UserNotification>> GetUserNotifications(int userId);
        Task<UserNotification?> GetByIdAsync(int userNotificationId);
        Task UpdateAsync(UserNotification userNotification);

    }
}
