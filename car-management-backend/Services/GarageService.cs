
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Services.Interfaces;
using car_management_backend.Utilities.Helpers;

namespace car_management_backend.Services
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepo;
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

        public IEnumerable<ResponseGarageDto> GetAllGarages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GarageDailyAvailabilityReportDto> DailyAvailabilityReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }
    }
}
