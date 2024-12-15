using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Repositories.Interfaces
{
    public interface IMaintenanceRepository
    {
        Maintenance GetMaintenaceById(int id);
        IEnumerable<Maintenance> GetAllMaintenaces();
        IEnumerable<Maintenance> GetMaintenaceByCarId(int? carId);
        IEnumerable<Maintenance> GetMaintenaceByGarageId(int? garageId);
        IEnumerable<Maintenance> GetMaintenanceFromYearToYear(DateTime? startDate, DateTime? endDate);
        IEnumerable<Maintenance> MonthlyRequestsReport(int? garageId, DateTime? startDate, DateTime? endDate);
        Maintenance AddNewMaintenace(Maintenance maintenance);
        void UpdateMaintenace(Maintenance maintenance);
        void DeleteMaintenace(int id);
        void SaveChanges();
    }
}
