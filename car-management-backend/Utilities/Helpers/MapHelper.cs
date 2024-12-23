using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using car_management_backend.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace car_management_backend.Utilities.Helpers
{
    public class MapHelper
    {
        public static CreateCarDto MapCreateCarToDto(Car car)
        {
            return new CreateCarDto()
            {
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate, 
                GarageIds = car.CarGarages.Select(cg => cg.GarageId).ToList()
            };
        }

        public static UpdateCarDto MapUpdateCarToDto(Car car)
        {
            return new UpdateCarDto()
            {
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                GarageIds = car.CarGarages.Select(cg => cg.GarageId).ToList()
            };
        }

        public static ResponseCarDto MapResponseCarToDto(Car car)
        {
            return new ResponseCarDto()
            {
                Id = car.CarId,
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                Garages = car.CarGarages.Select(cg => new ResponseGarageDto()
                {
                    Id = cg.GarageId,
                    Name = cg.Garage.Name,
                    Location = cg.Garage.Location,
                    City = cg.Garage.City,
                    Capacity = cg.Garage.Capacity
                }).ToList()
            };
        }

        public static CreateGarageDto MapCreateGarageToDto(Garage garage)
        {
            return new CreateGarageDto()
            {
                Name = garage.Name,
                Location = garage.Location,
                City = garage.City,
                Capacity = garage.Capacity
            };
        }

        public static UpdateGarageDto MapUpdateGarageToDto(Garage garage)
        {
            return new UpdateGarageDto()
            {
                Name = garage.Name,
                Location = garage.Location,
                City = garage.City,
                Capacity = garage.Capacity
            };
        }

        public static ResponseGarageDto MapResponseGarageToDto(Garage garage)
        {
            return new ResponseGarageDto()
            {
                Id = garage.GarageId,
                Name = garage.Name,
                Location = garage.Location,
                City = garage.City,
                Capacity = garage.Capacity
            };
        }

        public static GarageDailyAvailabilityReportDto MapGarageDailyAvailabilityReportToDto(GarageReport garageReport)
        {
            return new GarageDailyAvailabilityReportDto()
            {
                Date = garageReport.Date,
                Requests = garageReport.Requests,
                AvailableCapacity = garageReport.AvailableCapacity,
            };

        }

        public static CreateMaintenanceDto MapCreateMaintenanceToDto(Maintenance maintenance)
        {
            return new CreateMaintenanceDto()
            {
                CarId = maintenance.Car.CarId,
                ServiceType = maintenance.ServiceType,
                ScheduledDate = maintenance.ScheduledDate,
                GarageId = maintenance.Garage.GarageId

            };
        }

        public static UpdateMaintenanceDto MapUpdateMaintenanceToDto(Maintenance maintenance)
        {
            return new UpdateMaintenanceDto()
            {
                CarId = maintenance.Car.CarId,
                ServiceType = maintenance.ServiceType,
                ScheduledDate = maintenance.ScheduledDate,
                GarageId = maintenance.Garage.GarageId

            };
        }

        public static ResponseMaintenanceDto MapResponseMaintenanceToDto(Maintenance maintenance)
        {
            return new ResponseMaintenanceDto()
            {
                Id = maintenance.Id,
                CarId = maintenance.CarId,
                CarName = maintenance.CarName,
                ServiceType = maintenance.ServiceType,
                ScheduledDate = maintenance.ScheduledDate,
                GarageId = maintenance.GarageId,
                GarageName = maintenance.GarageName
            };
        }

        public static MonthlyRequestsReportDto MapMonthlyRequestsReportDto(MaintenanceReport maintenanceReport) 
        {
            var yearMonth = new YearMonth
            {
                Year = maintenanceReport.YearMonth.Year,
                Month = maintenanceReport.YearMonth.Month ?? CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(maintenanceReport.YearMonth.MonthValue).ToUpper(),
                LeapYear = DateTime.IsLeapYear(maintenanceReport.YearMonth.Year),
                MonthValue = maintenanceReport.YearMonth.MonthValue
            };
            return new MonthlyRequestsReportDto()
            {
                YearMonth = yearMonth,
                Requests = maintenanceReport.Requests
            };
        }

    }
}
