using BookingForHumanService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Entities
{
   public class Notification
    {
       

        public int Id { get; private set; }

        public TargetType TargetType { get; private set; }
        public NotificationType Type { get; private set; }

        public string Title { get; private set; }
        public string Message { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private Notification() { }

        public Notification( string title, string message, TargetType targetType, NotificationType type)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required");
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message is required");

            if (!Enum.IsDefined(typeof(NotificationType), type)) throw new ArgumentException(" Notification Type Is UnDefined");
            if (!Enum.IsDefined(typeof(TargetType), targetType)) throw new ArgumentException(" Reciever Type Is UnDefined");


            Title = title;
            TargetType =  targetType;
            Type = type;
            Message = message;
            CreatedAt = DateTime.UtcNow;
        }

    
    }
}
    
