
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Services.Interfaces;
using car_management_backend.Utilities.Helpers;
using System.Globalization;

namespace car_management_backend.Services
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepo;
        private static int _apiGarageCallCount = 0;
        private static DateTime dateTimeNow = DateTime.Now;
        public GarageService(IGarageRepository garageRepo)
        {
            _garageRepo = garageRepo;
        }

        public ResponseGarageDto GetGarage(int id)
        {
          
            _apiGarageCallCount++;
           
            var garages = _garageRepo.GetGarageById(id);
            return MapHelper.MapResponseGarageToDto(garages);
        }

        public IEnumerable<ResponseGarageDto> GetAllGarages(string? city)
        {

            if (city != null)
            {
                _apiGarageCallCount++;
                return GetGaragesByCity(city);
            }

            var garages = _garageRepo.GetAllGarages();
            _apiGarageCallCount++;
            return garages.Select(c => MapHelper.MapResponseGarageToDto(c));
        }

        public IEnumerable<ResponseGarageDto> GetGaragesByCity(string? city)
        {
            var garages = _garageRepo.GetGaragesByCity(city);
            _apiGarageCallCount++;
            return garages.Select(c => MapHelper.MapResponseGarageToDto(c));
        }

        public void CreateGarage(CreateGarageDto garageDto)
        {
            _apiGarageCallCount++;
            var garage = new Garage
            {
                Name = garageDto.Name,
                Location = garageDto.Location,
                Capacity = garageDto.Capacity,
                City = garageDto.City
            };
            _garageRepo.AddGarage(garage);
            _garageRepo.SaveChanges();
            
            MapHelper.MapCreateGarageToDto(garage);
        }

        public void UpdateGarage(int id, UpdateGarageDto garageDto)
        {
            _apiGarageCallCount++;
            Garage garageId = new Garage();
            garageId.GarageId = id;
            var garage = _garageRepo.GetGarageById(garageId.GarageId);


            if (garage != null)
            {
                garage.Name = garageDto.Name;
                garage.Location = garageDto.Location;
                garage.Capacity = garageDto.Capacity;
                garage.City = garageDto.City;
                _garageRepo.UpdateGarage(garage);
                _garageRepo.SaveChanges();
                MapHelper.MapUpdateGarageToDto(garage);
            }
        }

        public void DeleteGarage(int id)
        {
            _apiGarageCallCount++;
            _garageRepo.DeleteGarage(id);
            _garageRepo.SaveChanges();
        }

        public IEnumerable<GarageDailyAvailabilityReportDto> DailyAvailabilityReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
            _apiGarageCallCount++;

            var result = _garageRepo.GetGarageById((int)garageId);
            // Fetch daily availability reports from the repository
            var dailyAvailabilityReports = _garageRepo.DailyAvailabilityReport(garageId, startDate, endDate);
           

            // Ensure garageId is valid and fetch the garage details
            if (!garageId.HasValue)
                throw new ArgumentException("GarageId must be provided.");

            var garage = _garageRepo.GetGarageById(garageId.Value);
            if (garage == null)
                throw new InvalidOperationException($"Garage with ID {garageId.Value} not found.");

            // Calculate the AvailableCapacity for each report
            var reportDtos = dailyAvailabilityReports.Select(dailyAvailability => new GarageDailyAvailabilityReportDto
            {
                Date = dailyAvailability.Date,
                Requests = _apiGarageCallCount,
                AvailableCapacity = garage.Capacity - garage.CarGarages.Count() // Garage capacity minus cars currently in garage
            }).ToList();

            return reportDtos;

        }


    }
}
