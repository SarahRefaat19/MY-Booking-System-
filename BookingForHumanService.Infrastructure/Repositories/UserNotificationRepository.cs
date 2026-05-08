using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Infrastructure.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly BookingDbContext _context;

        public UserNotificationRepository(BookingDbContext context)
        {
            _context = context;
        }
        public async Task AddRangeAsync(List<UserNotification> notifications)
        {
            await _context.UserNotifications.AddRangeAsync(notifications);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserNotification>> GetUserNotifications(int userId)
        {
            var userNotifications = await _context.UserNotifications.Where(a => a.UserId == userId).ToListAsync();
            return userNotifications;


        }

        public async Task<UserNotification?> GetByIdAsync(int Id)
        {
            var userNotifications = await _context.UserNotifications.FindAsync(Id);
            return userNotifications;


        }
   public async  Task UpdateAsync(UserNotification userNotification)
        {
             _context.UserNotifications.Update(userNotification);
        }

    }
}
