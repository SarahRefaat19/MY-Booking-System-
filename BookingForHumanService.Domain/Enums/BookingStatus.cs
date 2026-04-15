using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Domain.Enums
{
  public enum BookingStatus
    {
        Pending = 1,      
        Accepted = 2,      
        Rejected = 3,      
        Cancelled = 4,      
        InProgress = 5,    
        Completed = 6      
    }
}
