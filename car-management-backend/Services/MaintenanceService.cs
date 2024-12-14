using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using car_management_backend.Services.Interfaces;

namespace car_management_backend.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        public void AddNewMaintenace(CreateMaintenanceDto maintenanceDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteMaintenace(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResponseGarageDto> GetAllMaintenaces(int? carId, int? garageId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public ResponseMaintenanceDto GetMaintenaceById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MonthlyRequestsReportDto> MonthlyRequestsReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateMaintenace(int id, UpdateMaintenanceDto maintenanceDto)
        {
            throw new NotImplementedException();
        }
    }
}
