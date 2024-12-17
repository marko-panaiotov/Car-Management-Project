
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

            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations(); // Make sure annotations are enabled for Swagger
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
