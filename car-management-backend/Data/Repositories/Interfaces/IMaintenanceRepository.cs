using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Repositories.Interfaces
{
    public interface IMaintenanceRepository
    {
        Maintenance GetMaintenaceById(int id);
        IEnumerable<Maintenance> GetAllMaintenaces(int? carId, int? garageId, DateTime? startDate, DateTime? endDate);
        // IEnumerable<ResponseGarageDto> GetGaragesByCity(string? city);
        IEnumerable<Maintenance> MonthlyRequestsReport(int? garageId, DateTime? startDate, DateTime? endDate);
        void AddNewMaintenace(Maintenance maintenanceDto);
        void UpdateMaintenace(int id, Maintenance maintenanceDto);
        void DeleteMaintenace(int id);
    }
}
