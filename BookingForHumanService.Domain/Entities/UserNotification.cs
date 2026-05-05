using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Domain.Entities
{
    public class UserNotification
    {
        public int Id { get; private set; }

        public string UserId { get; private set; }
        public int NotificationId { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }

        public Notification Notification { get; private set; }



        public void MarkAsRead()
        {
            if(IsRead) return ;

            IsRead = true;
            ReadAt = DateTime.UtcNow;

        }







    }

}

