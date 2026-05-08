using BookingForHumanService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Domain.Interfaces
{
    public interface IUserNotification
    {
        Task AddRangeAsync(List<UserNotification> notifications);
        Task<List<UserNotification>> GetUserNotifications(string userId);

    }
}
