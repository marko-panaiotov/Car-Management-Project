﻿using Azure.Identity;
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Services.Interfaces;
using car_management_backend.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace car_management_backend.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepo;
        private readonly ICarRepository _carRepo;
        private readonly IGarageRepository _garageRepo;

        public MaintenanceService(IMaintenanceRepository maintenanceRepo, ICarRepository carRepo, IGarageRepository garageRepo)
        {
            _carRepo = carRepo;
            _garageRepo = garageRepo;
            _maintenanceRepo = maintenanceRepo;
        }
        public void AddNewMaintenace(CreateMaintenanceDto maintenanceDto)
        {
            var car = _carRepo.GetCarById(maintenanceDto.CarId);

            var garage = _garageRepo.GetGarageById(maintenanceDto.GarageId);

            maintenanceDto.CarId = car.CarId;
            maintenanceDto.GarageId = garage.GarageId;

            var maintenance = new Maintenance
            {
                GarageId = maintenanceDto.GarageId,
                GarageName = garage.Name,
                CarId = maintenanceDto.CarId,
                CarName = car.Make,
                ServiceType = maintenanceDto.ServiceType,
                ScheduledDate = maintenanceDto.ScheduledDate
            };

            _maintenanceRepo.AddNewMaintenance(maintenance);
            _maintenanceRepo.SaveChanges();

            MapHelper.MapCreateMaintenanceToDto(maintenance);
        }

        public void DeleteMaintenace(int id)
        {
            _maintenanceRepo.DeleteMaintenance(id);
            _maintenanceRepo.SaveChanges();
        }

        public IEnumerable<ResponseMaintenanceDto> GetAllMaintenances(int? carId, int? garageId, DateTime? startDate, DateTime? endDate)
        {
            var test= startDate;
            var test3 =endDate;
            if (carId != null || garageId != null || startDate.HasValue && endDate.HasValue)
            {
                if (carId != null)
                {
                    return GetMaintenaceByCarId(carId);
                }
                if (garageId != null)
                {
                    return GetMaintenaceByGarageId(garageId);
                }
                if (startDate.HasValue && endDate.HasValue)
                {
                    return GetMaintenanceFromYearToYear(startDate, endDate);
                }
            }

            var maintenances = _maintenanceRepo.GetAllMaintenances();
            return maintenances.Select(c => MapHelper.MapResponseMaintenanceToDto(c));
        }

        public IEnumerable<ResponseMaintenanceDto> GetMaintenaceByCarId(int? carId)
        {
            var maintenance = _maintenanceRepo.GetMaintenanceByCarId(carId);
            return maintenance.Select(c => MapHelper.MapResponseMaintenanceToDto(c));
        }

        public IEnumerable<ResponseMaintenanceDto> GetMaintenaceByGarageId(int? garageId)
        {
            var maintenance = _maintenanceRepo.GetMaintenanceByGarageId(garageId);
            return maintenance.Select(c => MapHelper.MapResponseMaintenanceToDto(c));
        }

        public ResponseMaintenanceDto GetMaintenaceById(int id)
        {
            var maintenance = _maintenanceRepo.GetMaintenaceById(id);
            return MapHelper.MapResponseMaintenanceToDto(maintenance);
        }

        public IEnumerable<ResponseMaintenanceDto> GetMaintenanceFromYearToYear(DateTime? startDate, DateTime? endDate)
        {
            var maintenance = _maintenanceRepo.GetMaintenanceFromYearToYear(startDate,endDate);
            return maintenance.Select(c => MapHelper.MapResponseMaintenanceToDto(c));
        }

        public void UpdateMaintenace(int id, UpdateMaintenanceDto maintenanceDto)
        {
            var maintenance = _maintenanceRepo.GetMaintenaceById(id);
            var car = _carRepo.GetCarById(maintenanceDto.CarId);
            var garage = _garageRepo.GetGarageById(maintenanceDto.GarageId);

            if (maintenance != null && car != null && garage != null)
            {
                maintenance.GarageId = garage.GarageId;
                maintenance.GarageName = garage.Name;
                maintenance.CarId = car.CarId;
                maintenance.CarName = car.Make;
                maintenance.ServiceType = maintenanceDto.ServiceType;
                maintenance.ScheduledDate = maintenanceDto.ScheduledDate;

                _maintenanceRepo.UpdateMaintenance(maintenance);
                _maintenanceRepo.SaveChanges();
                MapHelper.MapUpdateMaintenanceToDto(maintenance);
            }

        }

        public IEnumerable<MonthlyRequestsReportDto> MonthlyRequestsReport(int? garageId, string startDate, string endDate)
        {

            var report = _maintenanceRepo.MonthlyRequestsReport(garageId, startDate, endDate);
            var reportDtos = report.Select(dailyAvailability => new MonthlyRequestsReportDto
            {
                YearMonth = new YearMonth()
                {

                    //Year = dailyAvailability.YearMonth.Year,
                    Month = dailyAvailability.YearMonth.Month,
                   //LeapYear = dailyAvailability.YearMonth.LeapYear,
                   // MonthValue = dailyAvailability.YearMonth.MonthValue
                },
                Requests = dailyAvailability.Requests
            }).ToList();


            return report.Select(r => MapHelper.MapMonthlyRequestsReportDto(r));
        }  
    }
}
