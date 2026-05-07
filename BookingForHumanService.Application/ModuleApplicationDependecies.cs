using BookingForHumanService.Application.Mapping;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Application.Services;


using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentValidation;
using BookingForHumanService.Domain.Interfaces;

namespace BookingForHumanService.Application
{
    public static class ModuleApplicationDependecies
    {
        public static IServiceCollection AddApplicationDependecies(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddAutoMapper(typeof(CustomerMapping));
            services.AddAutoMapper(typeof(ProviderMapping));
            services.AddAutoMapper(typeof(ReviewProfile));

            services.AddValidatorsFromAssembly(typeof(ModuleApplicationDependecies).Assembly);

            return services;
        }
    }
}
