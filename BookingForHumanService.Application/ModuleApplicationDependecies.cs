using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Application.Mapping;
using BookingForHumanService.Application.Services;
using BookingForHumanService.Application.UseCases.BookingUseCases.AcceptBookingUseCase;
using BookingForHumanService.Application.UseCases.BookingUseCases.RejectBookingUseCase;
using BookingForHumanService.Application.UseCases.CustomerUseCases;
using BookingForHumanService.Application.UseCases.ProviderUseCases.SetAvailabilityUseCase;
using BookingForHumanService.Application.UseCases.ProviderUseCases.UpdateProfileUseCase;
using BookingForHumanService.Domain.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BookingForHumanService.Application
{
    public static class ModuleApplicationDependecies
    {
        public static IServiceCollection AddApplicationDependecies(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAcceptBookingUseCase, AcceptBooking>();
            services.AddScoped<IRejectBookingUseCase, RejectBooking>();
            services.AddScoped<ChangeStatusUseCase>();
            services.AddScoped<UpdateProfileUseCase>();
            services.AddScoped<ISetAvailability, SetAvailabilty>();
            services.AddScoped<IUpdateProfileUseCase, UpdateProfile>();

            services.AddAutoMapper(typeof(CustomerMapping));
            services.AddAutoMapper(typeof(ProviderMapping));
            services.AddAutoMapper(typeof(ReviewProfile));

            services.AddValidatorsFromAssembly(typeof(ModuleApplicationDependecies).Assembly);

            return services;
        }
    }
}
