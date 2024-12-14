using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using Microsoft.AspNetCore.Mvc;

namespace car_management_backend.Services.Interfaces
{
    public interface IMaintenanceService
    {
        ResponseMaintenanceDto GetMaintenaceById(int id);
        IEnumerable<ResponseGarageDto> GetAllMaintenaces(int? carId, int? garageId, DateTime? startDate, DateTime? endDate);
       // IEnumerable<ResponseGarageDto> GetGaragesByCity(string? city);
        IEnumerable<MonthlyRequestsReportDto> MonthlyRequestsReport(int? garageId, DateTime? startDate, DateTime? endDate);
        void AddNewMaintenace(CreateMaintenanceDto maintenanceDto);
        void UpdateMaintenace(int id, UpdateMaintenanceDto maintenanceDto);
        void DeleteMaintenace(int id);
    }
}
