
using BookingForHumanService.Application;
using BookingForHumanService.Infrastructure;
using System;
using Serilog;
namespace BookingForHumanService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

Log.Logger =new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console().WriteTo.File("Logs/log-.txt", rollingInterval:RollingInterval.Day ).CreateLogger();
            builder.Host.UseSerilog();

            builder.Services.BookingDbContext<BookingDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // add application and infrastructure dependencies
            builder.Services.AddApplicationDependecies();
            builder.Services.AddInfrastructureDependecies(builder.Configuration);


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
