using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using car_management_backend.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace car_management_backend.Services.Interfaces
{
    public interface IMaintenanceService
    {
        ResponseMaintenanceDto GetMaintenaceById(int id);
        IEnumerable<ResponseMaintenanceDto> GetAllMaintenaces(int? carId, int? garageId, DateTime? startDate, DateTime? endDate);
        IEnumerable<ResponseMaintenanceDto> GetMaintenaceByCarId(int? carId);
        IEnumerable<ResponseMaintenanceDto> GetMaintenaceByGarageId(int? garageId);
        IEnumerable<ResponseMaintenanceDto> GetMaintenanceFromYearToYear(DateTime? startDate, DateTime? endDate);
        IEnumerable<MonthlyRequestsReportDto> MonthlyRequestsReport(int? garageId, DateTime? startDate, DateTime? endDate);
        void AddNewMaintenace(CreateMaintenanceDto maintenanceDto);
        void UpdateMaintenace(int id, UpdateMaintenanceDto maintenanceDto);
        void DeleteMaintenace(int id);
    }
}
