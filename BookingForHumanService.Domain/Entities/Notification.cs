using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Entities
{
   public class Notification
    {
       

          public int Id { get; private set; }

        public int UserId { get; private set; }  
        public string Title { get; private set; }
        public string Message { get; private set; }

        public bool IsRead { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Notification() { }

        public Notification(int userId, string title, string message)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required");
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message is required");

            UserId = userId;
            Title = title;
            Message = message;
            IsRead = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}
    
