using BookingForHumanService.Application;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Infrastructure;
using BookingForHumanService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
namespace BookingForHumanService.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            // Register Identity Services at DI Container 
            // UserManger - SingInManger - RoleManger - PasswordHasher - Validators ...

            //builder.Services.AddIdentity<User, IdentityRole<int>>()
            //    .AddEntityFrameworkStores<BookingDbContext>() // هنا بقوله يخزن فين
            //    .AddDefaultTokenProviders();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();
        
            // add application and infrastructure dependencies
            builder.Services.AddApplicationDependecies();
            builder.Services.AddInfrastructureDependecies(builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<BookingDbContext>();

                await dbContext.Database.MigrateAsync(); // برضه عشان لو رفعنا على سيرفر الداتا بيز تتعمل
            }


            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
               
                // app.UseSwagger();
              //  app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSerilogRequestLogging();
            app.MapControllers();

            app.Run();

        }
    }
}
