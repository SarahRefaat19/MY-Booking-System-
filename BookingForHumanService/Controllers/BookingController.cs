using BookingForHumanService.API.Response;
using BookingForHumanService.Application.DTOs;
using BookingForHumanService.Application.DTOs.BookingDtos;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Application.Services;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BookingForHumanService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        [HttpPost]
        public async Task<ActionResult<ReadBookingDto>> CreateBooking(AddBookingDto dto)
        {
            var booking = await _bookingService.CreateBookingAsync(
                dto.CustomerId,
                dto.ProviderId,
                dto.ServiceDate,
                dto.Price,
                dto.Details
            );

            return Ok(new ApiResponse<Booking>
            {
                Success = true,
                Message = "Success",
                Data = booking
            });
        }


        [HttpPost("{id}/accept")]
        public async Task<IActionResult> Accept(int id)
        {
            await _bookingService.AcceptBookingAsync(id);
            return Ok("Booking accepted");
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
            await _bookingService.RejectBookingAsync(id);
            return Ok("Booking rejected");
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            await _bookingService.StartBookingAsync(id);
            return Ok("Booking started");
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _bookingService.CancelBookingAsync(id);
            return Ok("Booking cancelled");
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            await _bookingService.CompleteBookingAsync(id);
            return Ok("Booking completed");
        }


        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            var bookings = await _bookingService.GetByCustomerAsync(customerId);
            return Ok(bookings);
        }

        [HttpGet("provider/{providerId}")]
        public async Task<IActionResult> GetByProvider(int providerId)
        {
            var bookings = await _bookingService.GetByProviderAsync(providerId);
            return Ok(bookings);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetByStatus([FromQuery] BookingStatus status)
        {
            var bookings = await _bookingService.GetByStatusAsync(status);
            return Ok(bookings);
        }
    }
}