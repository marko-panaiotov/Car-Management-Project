using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Repositories.Interfaces
{
    public interface IMaintenanceRepository
    {
        Maintenance GetMaintenaceById(int id);
        IEnumerable<Maintenance> GetAllMaintenances();
        IEnumerable<Maintenance> GetMaintenanceByCarId(int? carId);
        IEnumerable<Maintenance> GetMaintenanceByGarageId(int? garageId);
        IEnumerable<Maintenance> GetMaintenanceFromYearToYear(DateTime? startDate, DateTime? endDate);
        IEnumerable<MaintenanceReport> MonthlyRequestsReport(int? garageId, string startDate, string endDate);
        Maintenance AddNewMaintenance(Maintenance maintenance);
        void UpdateMaintenance(Maintenance maintenance);
        void DeleteMaintenance(int id);
        void SaveChanges();
    }
}
