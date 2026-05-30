using BookingForHumanService.API.Response;
using BookingForHumanService.Application.DTOs.ReviewDtos;
using BookingForHumanService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingForHumanService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles ="Customer")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ReadReviewDto>>> 
            AddReview([FromBody] AddReviewDto dto)
        {
            var reviewToReturn = await _reviewService.AddReviewAsync(dto);

            return Ok(ApiResponse<ReadReviewDto>.Ok(
                reviewToReturn,
                "Review created successfully"
                )
            );
        }

        [HttpGet("provider/{providerId}")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<ReadReviewDto>>>>
            GetProviderReviews(int providerId)
        {
            var reviews = await _reviewService.GetProviderReviewsAsync(providerId);
            return Ok(ApiResponse<IReadOnlyList<ReadReviewDto>>.Ok(reviews));
        }


        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<ReadReviewDto>>>>
            GetCustomerReviews(int customerId)
        {
            var reviews = await _reviewService.GetCustomerReviewsAsync(customerId);
            return Ok(ApiResponse<IReadOnlyList<ReadReviewDto>>.Ok(reviews));
        }

        [Authorize(Roles ="Customer")]
        [HttpPut("{reviewId}")]
        public async Task<ActionResult<ApiResponse<ReadReviewDto>>> 
            UpdateReview(int reviewId, [FromBody]UpdateReviewDto dto)
        {
            var updatedReview = await _reviewService.UpdateReviewAsync(reviewId, dto);

            return Ok(ApiResponse<ReadReviewDto>.Ok(updatedReview, "Review Updated Successfully"));
        }


        [HttpDelete("{reviewId}")]
        [Authorize(Roles ="Customer,Admin")]
        public async Task<ActionResult<ApiResponse<string>>>
            DeleteReview(int reviewId)
        {
            await _reviewService.DeleteReviewAsync(reviewId);

            return Ok(ApiResponse<string>.Ok(null!, "Review Deleted"));
        }
    }
}
