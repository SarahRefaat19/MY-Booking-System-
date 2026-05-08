using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookingForHumanService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
     
            private readonly INotificationService _notificationService;

            public NotificationController(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            // GET api/notification/{userId}
            [HttpGet("{userId}")]
            public async Task<IActionResult> GetUserNotifications(int userId)
            {
                var notifications = await _notificationService.GetUserNotifications(userId);
                return Ok(notifications);
            }

            // PUT api/notification/markasread/{userNotificationId}
            [HttpPut("markasread/{userNotificationId}")]
            public async Task<IActionResult> MarkAsRead(int userNotificationId)
            {
                await _notificationService.MarkAsReadAsync(userNotificationId);
                return NoContent();
            }

            // POST api/notification/send/{specificUserId}
            [HttpPost("send/{specificUserId}")]
            public async Task<IActionResult> Send( [FromBody] Notification notification, [FromRoute] int specificUserId)
            {
                await _notificationService.SendByTargetUserType(notification, specificUserId);
                return Ok();
            }
        }

    }

