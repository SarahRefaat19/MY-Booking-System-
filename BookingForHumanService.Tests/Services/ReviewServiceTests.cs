using AutoMapper;
using BookingForHumanService.Application.DTOs.ReviewDtos;
using BookingForHumanService.Application.Services;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Domain.ValueObjects.ProviderValueObjects;
using BookingForHumanService.Domain.ValueObjects.CustomerValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Tests.Services
{
    public class ReviewServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReviewService _reviewService;

        public ReviewServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _reviewService = new ReviewService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task AddReviewAsync_ShouldCreateReview()
        {
            // Arrange

            var dto = new AddReviewDto
            {
                BookingId = 1,
                Rating = 4,
                Comment = "Good Man"
            };

            var user1 = User.Create(1, "Hasshh", UserRole.Provider);

            var provider = new Provider(
                user1,
                ProviderName.Create("Ahmed Refaat"),
                "Cleaning",
                ProviderEmail.Create("provider@test.com"),
                ProviderPhone.Create("01143564794"));


            var user2 = User.Create(2, "Hasshsh", UserRole.Customer);

            var customer = new Customer(
                user2,
                CustomerName.Create("Saraah Refaat"),
                CustomerEmail.Create("customer@test.com"),
                CustomerPhone.Create("01111111111"),
                CustomerStatus.Active);

            var booking = Booking.Create(customer, provider,
                DateTime.UtcNow.AddDays(1),
                100,
                "I need To Cleen My Room");


            booking.Accept();
            booking.Start();
            booking.Complete();


            var bookingRepoMock = new Mock<IBookingRepository>();

            var reviewRepoMock = new Mock<IReviewRepository>();


            _mockUnitOfWork.Setup(u => u.Bookings).Returns(bookingRepoMock.Object);
            _mockUnitOfWork.Setup(u => u.Reviews).Returns(reviewRepoMock.Object);

            bookingRepoMock.Setup(b => b.GetByIdAsync(dto.BookingId))
                .ReturnsAsync(booking);

            var readDto = new ReadReviewDto()
            {
                BookingId = dto.BookingId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow,
                CustomerId = customer.Id,
                ProviderId = provider.Id
            };

            _mockMapper.Setup(m => m.Map<ReadReviewDto>(It.IsAny<Review>()))
                .Returns(readDto);

            // ACT

            var result = await _reviewService.AddReviewAsync(dto);

            // Assert 

            Assert.NotNull(result);
            Assert.IsType<ReadReviewDto>(result);
            Assert.Equal(dto.Rating, result.Rating);
            Assert.Equal(dto.Comment, result.Comment);

            // Verify 
            reviewRepoMock.Verify(
                r => r.AddAsync(It.IsAny<Review>()),
                Times.Once);


            _mockUnitOfWork.Verify(
                u => u.SaveChangesAsync(),
                Times.Once);


        }


        [Fact]
        public async Task AddReviewAsync_WhenBookingNotFound_ShouldThrowException()
        {
            // Arrange

            var dto = new AddReviewDto
            {
                BookingId = 999,
                Rating = 5,
                Comment = "Excellent"
            };

            var bookingRepoMock = new Mock<IBookingRepository>();

            _mockUnitOfWork
                .Setup(u => u.Bookings)
                .Returns(bookingRepoMock.Object);

            bookingRepoMock
                .Setup(b => b.GetByIdAsync(dto.BookingId))
                .ReturnsAsync((Booking?)null);

            // Act & Assert 
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _reviewService.AddReviewAsync(dto));

            Assert.Equal("Booking not found", exception.Message);

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never); // متنادتش ولا مره

        }


    }
}
