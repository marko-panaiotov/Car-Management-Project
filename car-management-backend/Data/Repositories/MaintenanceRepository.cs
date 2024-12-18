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
        public IEnumerable<Maintenance> GetAllMaintenances()
        {
            return _dbContext.Maintenances
                .Include(m => m.Car) // Join with Car table
                .Include(m => m.Garage)
                /*.Select(m=> new Maintenance 
                { 
                    ServiceType = m.ServiceType,
                    ScheduledDate = m.ScheduledDate,
                   // GarageId = m.Garage.GarageId,
                    GarageName = m.Garage.Name,
                    //CarId = m.Car.CarId,
                    CarName = m.Car.Make,

                })*/
                .ToList();
            // throw new NotImplementedException();
        }

        public IEnumerable<Maintenance> GetMaintenanceByCarId(int? carId)
        {
            return _dbContext.Maintenances
               .Include(c => c.Car)
               .Include(g => g.Garage)
               .Where(c => c.Car.CarId == carId)
               .ToList();
        }

        public IEnumerable<Maintenance> GetMaintenanceByGarageId(int? garageId)
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
            return _dbContext.Maintenances
                 .Include(c => c.Car)
                 .Include(g => g.Garage)
                 .Where(c =>
                         c.ScheduledDate >= startDate.Value &&
                         c.ScheduledDate <= endDate.Value
                 )
                 .ToList();
        }

        public Maintenance AddNewMaintenance(Maintenance maintenance)
        {
            _dbContext.Maintenances.Add(maintenance);
            return maintenance;
        }

        public void UpdateMaintenance(Maintenance maintenance)
        {
            _dbContext.Maintenances.Update(maintenance);
        }


        public void DeleteMaintenance(int id)
        {
            var maintenanceToDelete = _dbContext.Maintenances
               .FirstOrDefault(c => c.Id == id);
            _dbContext.Maintenances.Remove(maintenanceToDelete);
        }
    
        public IEnumerable<Maintenance> MonthlyRequestsReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
           /* if (!startDate.HasValue || !endDate.HasValue)
                throw new ArgumentException("Start date and end date must be provided.");

            var today = DateTime.Now.Date;

            if (garageId.HasValue)
            {
                var garage= _dbContext.Garages
                    .Include(g => g.CarGarages)
                    .FirstOrDefault(g => g.GarageId == garageId.Value);

                if (garage == null)
                    throw new ArgumentException("Garage not found.");

                var todayReport = _dbContext.MaintenanceReports
                    .FirstOrDefault();

                var availableCapacity = garage.Capacity - garage.CarGarages.Count;

                if (todayReport != null)
                {
                    _apiGarageCallCount = _dbContext.GarageReports
                        .Where(r => r.GarageId == garageId.Value && r.Date == today)
                        .Select(r => r.Requests)
                        .FirstOrDefault();

                    todayReport.Requests = _apiGarageCallCount;
                    todayReport.AvailableCapacity = availableCapacity;

                    _dbContext.GarageReports.Update(todayReport);
                }
                else
                {
                    var newReport = new GarageReport
                    {
                        GarageId = garageId.Value,
                        Date = today,
                        Requests = 0,
                        AvailableCapacity = availableCapacity
                    };

                    _dbContext.GarageReports.Add(newReport);
                }

                _dbContext.SaveChanges();
            }

            var query = _dbContext.MaintenanceReports
                .Where(r => r.YearMonth.Month >= startDate.Value && r.Date <= endDate.Value);

            if (garageId.HasValue)
            {
                query = query.Where(r => r.GarageId == garageId.Value);
            }

            var garageData = _dbContext.Garages
                .Include(g => g.CarGarages)
                .ToDictionary(g => g.GarageId, g => new { g.Capacity, CarCount = g.CarGarages.Count });

            var stats = query.Select(r => new MaintenanceReport
            {
                YearMonth = r.,
                Date = r.Date,
                Requests = r.Requests,
                AvailableCapacity = (r.Date == today && garageId.HasValue && r.GarageId == garageId.Value)
                    ? garageData[r.GarageId].Capacity - garageData[r.GarageId].CarCount
                    : r.AvailableCapacity
            }).ToList();

            return stats;*/



             throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
