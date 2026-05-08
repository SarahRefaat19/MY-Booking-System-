using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookingForHumanService.Application.Services
{
    public  class NotificationService :INotificationService
    {
         
        private readonly INotificationRepository _notificationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IUserNotificationRepository _userNotificationRepository;

        // private static readonly Dictionary<NotificationType, List<TargetType>> Rules = new()
        // {

        //     { NotificationType.Transactional, new List<TargetType> { TargetType.Customer, TargetType.Provider } },
        //     { NotificationType.System,new List<TargetType> { TargetType.AllCustomers, TargetType.AllProviders } },
        //     { NotificationType.Markting, new List<TargetType> { TargetType.AllCustomers }},

        //};
        public NotificationService(INotificationRepository notificationRepository, IUserNotificationRepository usernotificationRepository, ICustomerRepository customerRepository, IProviderRepository providerRepository)
        {
            _notificationRepository = notificationRepository;
            _userNotificationRepository = usernotificationRepository;
            _customerRepository = customerRepository;   
            _providerRepository = providerRepository;
        }
     public async  Task<List<UserNotification>> GetUserNotifications(int userId)
        {
             return await _userNotificationRepository.GetUserNotifications(userId);
        }

        public async Task SendByTargetUserType( Notification notification, int specificUserId)
        {
            //Save In Db 
                  await _notificationRepository.AddAsync(notification);
            // Determine Notification Type And Whow Will Receive it using Rules 
            var receiverIds  = await GetUsersIdsByTargetType(notification.TargetType, specificUserId);


            // Make user Notification For Each Id 
            var batchSize = 1000;

            for (int i = 0; i < receiverIds.Count; i += batchSize)
            {
                var batch = receiverIds
                    .Skip(i)
                    .Take(batchSize)
                    .Select(x => new UserNotification(x, notification.Id))
                    .ToList();

                await _userNotificationRepository.AddRangeAsync(batch);
            }
            
        }


        private async Task<List<int>> GetUsersIdsByTargetType(TargetType targetType, int specificUserId)
        {
            return targetType switch
            {
                TargetType.AllProviders => (await _providerRepository.GetAllAsync()).Select(o => o.UserId).ToList(),
                TargetType.AllCustomers => (await _customerRepository.GetAllAsync()).Select(o => o.UserId).ToList(),

                TargetType.Customer =>
                    new List<int> { (await _customerRepository.GetByIdAsync(specificUserId)).UserId },

                TargetType.Provider =>
                    new List<int> { (await _providerRepository.GetByIdAsync(specificUserId)).UserId },
                _ => new List<int>()
            };
        }


        public async Task MarkAsReadAsync(int userNotificationId)
        {
            var usernotification = await _userNotificationRepository.GetByIdAsync(userNotificationId);
            if(usernotification == null)
            {
                throw new Exception($"Notification {userNotificationId} not found");

            }
            usernotification.MarkAsRead();

            await _userNotificationRepository.UpdateAsync(usernotification);

        }

    }
}
