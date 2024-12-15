using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Services.Interfaces;
using car_management_backend.Utilities.Helpers;
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
            var maintenance = new Maintenance
            {
                GarageId = garage.GarageId,
                GarageName = garage.Name,
                CarId = car.CarId,
                CarName = car.Make,
                ServiceType = maintenanceDto.ServiceType,
                ScheduledTime = maintenanceDto.ScheduledTime.ToString("yyyy-mm-dd")
            };

            _maintenanceRepo.AddNewMaintenace(maintenance);
            _maintenanceRepo.SaveChanges();
            MapHelper.MapCreateMaintenanceToDto(maintenance);
        }

        public void DeleteMaintenace(int id)
        {
            _maintenanceRepo.DeleteMaintenace(id);
            _maintenanceRepo.SaveChanges();
        }

        public IEnumerable<ResponseMaintenanceDto> GetAllMaintenaces(int? carId, int? garageId, DateTime? startDate, DateTime? endDate)
        {
            if (carId != null || garageId != 0 || startDate != null && endDate != null)
            {
                if (carId != null)
                {
                    return GetMaintenaceByCarId(carId);
                }
                if (garageId != 0)
                {
                    return GetMaintenaceByGarageId(garageId);
                }
                if (startDate!= null && endDate != null)
                {
                    return GetMaintenanceFromYearToYear(startDate,endDate);
                }
            }
            var maintenances = _maintenanceRepo.GetAllMaintenaces();
            return maintenances.Select(c => MapHelper.MapResponseMaintenanceToDto(c));
        }

        public IEnumerable<ResponseMaintenanceDto> GetMaintenaceByCarId(int? carId)
        {
            var maintenance = _maintenanceRepo.GetMaintenaceByCarId(carId);
            return maintenance.Select(c => MapHelper.MapResponseMaintenanceToDto(c));
        }

        public IEnumerable<ResponseMaintenanceDto> GetMaintenaceByGarageId(int? garageId)
        {
            var maintenance = _maintenanceRepo.GetMaintenaceByGarageId(garageId);
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

        public IEnumerable<MonthlyRequestsReportDto> MonthlyRequestsReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateMaintenace(int id, UpdateMaintenanceDto maintenanceDto)
        {
            var maintenance = _maintenanceRepo.GetMaintenaceById(id);
            var car = _carRepo.GetCarById(maintenanceDto.CarId);
            var garage = _garageRepo.GetGarageById(maintenanceDto.GarageId);

            maintenance.GarageId = garage.GarageId;
            maintenance.GarageName = garage.Name;
            maintenance.CarId = car.CarId;
            maintenance.CarName = car.Make;
            maintenance.ServiceType = maintenanceDto.ServiceType;
            maintenance.ScheduledTime = maintenanceDto.ScheduledTime.ToString("yyyy-mm-dd");

            _maintenanceRepo.UpdateMaintenace(maintenance);
            _maintenanceRepo.SaveChanges();
        }
    }
}
