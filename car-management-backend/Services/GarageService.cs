
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
        private static DateTime dateTimeNow = DateTime.Now;
        public GarageService(IGarageRepository garageRepo)
        {
            _garageRepo = garageRepo;
        }

        public ResponseGarageDto GetGarage(int id)
        {
            var garages = _garageRepo.GetGarageById(id);

            return MapHelper.MapResponseGarageToDto(garages);
        }

        public IEnumerable<ResponseGarageDto> GetAllGarages(string? city)
        {

            if (city != null)
            {
                
                return GetGaragesByCity(city);
            }

            var garages = _garageRepo.GetAllGarages();
            
            return garages.Select(c => MapHelper.MapResponseGarageToDto(c));
        }

        public IEnumerable<ResponseGarageDto> GetGaragesByCity(string? city)
        {
            var garages = _garageRepo.GetGaragesByCity(city);
            
            return garages.Select(c => MapHelper.MapResponseGarageToDto(c));
        }

        public void CreateGarage(CreateGarageDto garageDto)
        {
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
            _garageRepo.DeleteGarage(id);
            _garageRepo.SaveChanges();
        }

        public IEnumerable<GarageDailyAvailabilityReportDto> DailyAvailabilityReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
            /*  GarageReport garageReport = new GarageReport();

              var result = _garageRepo.GetGarageById((int)garageId);

              var dailyAvailabilityReports = _garageRepo.DailyAvailabilityReport(garageId, startDate, endDate);

              if (!garageId.HasValue)
                  throw new ArgumentException("GarageId must be provided.");

              var garage = _garageRepo.GetGarageById(garageId.Value);
              if (garage == null)
                  throw new InvalidOperationException($"Garage with ID {garageId.Value} not found.");

              var reportDtos = dailyAvailabilityReports.Select(dailyAvailability => new GarageDailyAvailabilityReportDto
              {
                  Date = dailyAvailability.Date,
                  Requests = dailyAvailability.Requests,
                  AvailableCapacity=dailyAvailability.AvailableCapacity                                                             
              }).ToList();


              MapHelper.MapGarageDailyAvailabilityReportToDto(dailyAvailabilityReports.FirstOrDefault());
              return reportDtos;*/
            GarageReport garageReport = new GarageReport();
            garageReport.Id = garageId.Value; 

            if (!garageId.HasValue)
                throw new ArgumentException("GarageId must be provided.");

            var garage = _garageRepo.GetGarageById(garageReport.Id);
            if (garage == null)
                throw new InvalidOperationException($"Garage with ID {garageId.Value} not found.");

            if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
                throw new ArgumentException("StartDate must be earlier than EndDate.");

            var dailyAvailabilityReports = _garageRepo.DailyAvailabilityReport(garageId, startDate, endDate);

            var reportDtos = dailyAvailabilityReports.Select(dailyAvailability => new GarageDailyAvailabilityReportDto
            {
                Date = dailyAvailability.Date, // Ensure the format is correct
                Requests = dailyAvailability.Requests,
                AvailableCapacity = dailyAvailability.AvailableCapacity
            }).ToList();

            // Optionally map the first element, if needed (but this seems unnecessary)
             MapHelper.MapGarageDailyAvailabilityReportToDto(garageReport);

            return reportDtos;

        }
    }
}
