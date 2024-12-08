
using car_management_backend.Data;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Data.Repositories;
using car_management_backend.Services.Interfaces;
using car_management_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<CarManagementDbContext>(options => options.UseSqlServer(connectionString));

            //Register services
            builder.Services.AddTransient<ICarRepository, CarRepository>();
            builder.Services.AddTransient<ICarService, CarService>();

            builder.Services.AddTransient<IGarageRepository, GarageRepository>();
            builder.Services.AddTransient<IGarageService, GarageService>();

            builder.Services.AddTransient<IMaintenanceRepository, MaintenanceRepository>();
            builder.Services.AddTransient<IMaintenanceService, MaintenanceService>();

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

            app.Run();
        }
    }
}
