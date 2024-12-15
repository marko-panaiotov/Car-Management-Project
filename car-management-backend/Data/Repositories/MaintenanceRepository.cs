using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace car_management_backend.Data.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly CarManagementDbContext _dbContext;

        public MaintenanceRepository(CarManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Maintenance> GetAllMaintenaces()
        {
            return _dbContext.Maintenances
                .Include(m => m.Car) // Join with Car table
                .Include(m => m.Garage)
                .Select(m=> new Maintenance 
                { 
                    ServiceType = m.ServiceType,
                    ScheduledTime = m.ScheduledTime,
                    GarageName = m.Garage.Name,
                    CarName = m.Car.Make,

                })
                     .ToList();
            // throw new NotImplementedException();
        }

        public IEnumerable<Maintenance> GetMaintenaceByCarId(int? carId)
        {
            return _dbContext.Maintenances
               .Include(c => c.Car)
               .Include(g => g.Garage)
               .Where(c => c.Car.CarId == carId)
               .ToList();
        }

        public IEnumerable<Maintenance> GetMaintenaceByGarageId(int? garageId)
        {
            return _dbContext.Maintenances
               .Include(c => c.Car)
               .Include(g => g.Garage)
               .Where(c => c.Garage.GarageId==garageId)
               .ToList();
        }

        public Maintenance GetMaintenaceById(int id)
        {
            return _dbContext.Maintenances
                .Include(c => c.Car)
                .Include(g=>g.Garage)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Maintenance> GetMaintenanceFromYearToYear(DateTime? startDate, DateTime? endDate)
        {
           /* return _dbContext.Maintenances
                 .Include(c => c.Car)
                 .Include (g => g.Garage)   
                 .Where(c =>
                         c.Car.ProductionYear >= startDate &&
                          c.Car.ProductionYear <= endDate
                 )
                 .ToList();*/
            throw new NotImplementedException();
        }

        public Maintenance AddNewMaintenace(Maintenance maintenance)
        {
            _dbContext.Maintenances.Add(maintenance);
            return maintenance;
        }

        public void UpdateMaintenace(Maintenance maintenance)
        {
            _dbContext.Maintenances.Update(maintenance);
        }


        public void DeleteMaintenace(int id)
        {
            var maintenanceToDelete = _dbContext.Maintenances
               .FirstOrDefault(c => c.Id == id);
            _dbContext.Maintenances.Remove(maintenanceToDelete);
        }
    
        public IEnumerable<Maintenance> MonthlyRequestsReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
