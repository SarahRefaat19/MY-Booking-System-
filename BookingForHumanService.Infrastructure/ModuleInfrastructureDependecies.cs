using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Infrastructure.Data;
using BookingForHumanService.Infrastructure.Repositories;
using BookingForHumanService.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingForHumanService.Infrastructure
{
    public static class ModuleInfrastructureDependecies
    {
        public static IServiceCollection AddInfrastructureDependecies(this IServiceCollection services, IConfiguration config)
        {
            // Register infrastructure services here
            services.AddDbContext<BookingDbContext>(options =>
           options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<INotificationRepository , NotificationRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();

            services.AddScoped<IUnitOfWork, UnitofWork>();


            return services;
        }

    }
}
