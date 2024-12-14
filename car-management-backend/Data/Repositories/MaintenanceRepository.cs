using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;

namespace car_management_backend.Data.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        public void AddNewMaintenace(Maintenance maintenanceDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteMaintenace(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Maintenance> GetAllMaintenaces(int? carId, int? garageId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public Maintenance GetMaintenaceById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Maintenance> MonthlyRequestsReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateMaintenace(int id, Maintenance maintenanceDto)
        {
            throw new NotImplementedException();
        }
    }
}
