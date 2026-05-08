using BookingForHumanService.Application.DTOs.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Interfaces
{
    public interface IReviewService
    {
        Task<ReadReviewDto> AddReviewAsync(AddReviewDto dto);

        Task<ReadReviewDto> UpdateReviewAsync(int reviewId, UpdateReviewDto dto);

        Task DeleteReviewAsync(int reviewId);

        Task<IReadOnlyList<ReadReviewDto>> GetProviderReviewsAsync(int providerId);

        Task<IReadOnlyList<ReadReviewDto>> GetCustomerReviewsAsync(int customerId);

        Task<ReadReviewDto> GetByIdAsync(int reviewId);
    }
}
