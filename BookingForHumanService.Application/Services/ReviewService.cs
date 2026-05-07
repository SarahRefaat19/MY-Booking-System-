using AutoMapper;
using BookingForHumanService.Application.DTOs.ReviewDtos;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.Interfaces;
using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork,
              IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReadReviewDto> AddReviewAsync(AddReviewDto reviewDto)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(reviewDto.BookingId);

            if (booking == null) throw new Exception("Booking not found");

            if (booking.Status != BookingStatus.Completed)
                throw new InvalidOperationException("Can not Review On InCompleted Bookings");

            if (booking.Review != null)
                throw new InvalidOperationException(
                    "Booking already reviewed");

            var review = new Review(
                booking,
                reviewDto.Rating,
                reviewDto.Comment
            );

            booking.AddReview(review);

            booking.Provider?.AddReview(review);

            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadReviewDto>(review);
        }

        public async Task DeleteReviewAsync(int reviewId) // Soft Deletee
        {
            var reviewToDelete = await _unitOfWork.Reviews.GetByIdAsync(reviewId);

            if (reviewToDelete == null) throw new Exception("Review Not Found");

            reviewToDelete.Deactivate();

            await _unitOfWork.Reviews.UpdateAsync(reviewToDelete);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ReadReviewDto> GetByIdAsync(int reviewId)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(reviewId);

            if (review == null)
                throw new Exception("Review not found");

            return _mapper.Map<ReadReviewDto>(review);
        }

        public async Task<IReadOnlyList<ReadReviewDto>> GetCustomerReviewsAsync(int customerId)
        {
            var reviews = await _unitOfWork.Reviews.GetByCustomerIdAsync(customerId);

            return _mapper.Map<IReadOnlyList<ReadReviewDto>>(reviews);
        }

        public async Task<IReadOnlyList<ReadReviewDto>> GetProviderReviewsAsync(int providerId)
        {
            var reviews = await _unitOfWork.Reviews.GetByProviderIdAsync(providerId);

            return _mapper.Map<IReadOnlyList<ReadReviewDto>>(reviews);
        }

        public async Task<ReadReviewDto> UpdateReviewAsync(int reviewId, UpdateReviewDto dto)
        {
            var reviewToUpdate = await _unitOfWork.Reviews.GetByIdAsync(reviewId);

            if (reviewToUpdate == null)
                throw new Exception("Review to update not found");

            reviewToUpdate.UpdateComment(dto.Comment);

            await _unitOfWork.Reviews.UpdateAsync(reviewToUpdate);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReadReviewDto>(reviewToUpdate);
        }
    }
}
