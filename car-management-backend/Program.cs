
using car_management_backend.Data;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Data.Repositories;
using car_management_backend.Services.Interfaces;
using car_management_backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using car_management_backend.Data.Dtos.GarageDtos;
using System.Text.Json;
using System.Globalization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using car_management_backend.Utilities.Parser;
using car_management_backend.Data.Entities;

namespace car_management_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));

            });

            builder.Services.AddControllers()
       .AddJsonOptions(options =>
       {
           // Register the custom DateTimeConverter
           options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd"));
       });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();

            var currentMonth = DateTime.Now.ToString("MMMM").ToUpper(); // "JANUARY", "FEBRUARY", etc.


            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations(); // Make sure annotations are enabled for Swagger

                c.MapType<YearMonth>(() => new OpenApiSchema
                {
                    Type = "object",
                    Properties =
            {
            ["year"] = new OpenApiSchema
            {
                Type = "integer",
                Example = new OpenApiInteger(0)
            },
            ["month"] = new OpenApiSchema
            {
                   Type = "string",
                Enum = new[]
                {
                    new OpenApiString("JANUARY"),
                    new OpenApiString("FEBRUARY"),
                    new OpenApiString("MARCH"),
                    new OpenApiString("APRIL"),
                    new OpenApiString("MAY"),
                    new OpenApiString("JUNE"),
                    new OpenApiString("JULY"),
                    new OpenApiString("AUGUST"),
                    new OpenApiString("SEPTEMBER"),
                    new OpenApiString("OCTOBER"),
                    new OpenApiString("NOVEMBER"),
                    new OpenApiString("DECEMBER")
                },
                Example = new OpenApiString(currentMonth) // Set the current month dynamically
            },
            ["leapYear"] = new OpenApiSchema
            {
                Type = "boolean",
                Example = new OpenApiBoolean(true)
            },
            ["monthValue"] = new OpenApiSchema
            {
                Type = "integer",
                Example = new OpenApiInteger(0)
                }
            }
                }); ;

                c.MapType<DateTime>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date",
                   // Example = new OpenApiString("2024-01-01")
                });
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<CarManagementDbContext>(options => options.UseSqlServer(connectionString));

            //Register services
            builder.Services.AddTransient<ICarRepository, CarRepository>();
            builder.Services.AddTransient<ICarService, CarService>();

            builder.Services.AddTransient<IGarageRepository, GarageRepository>();
            builder.Services.AddTransient<IGarageService, GarageService>();

            builder.Services.AddTransient<IMaintenanceRepository, MaintenanceRepository>();
            builder.Services.AddTransient<IMaintenanceService, MaintenanceService>();

            builder.Services.AddCors();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors(builder =>
             builder.WithOrigins("http://localhost:3000") // React app URL
              .AllowAnyMethod()
              .AllowAnyHeader());
            app.UseRouting();

            app.Run();
        }
    }
}
