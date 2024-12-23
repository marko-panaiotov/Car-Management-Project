using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Globalization;

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
                .Include(m => m.Car)
                .Include(m => m.Garage)
                .ToList();
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

        public IEnumerable<MaintenanceReport> MonthlyRequestsReport(int? garageId, string startDate, string endDate)
        {
            if (!DateOnly.TryParseExact(startDate, "yyyy-MM", null, DateTimeStyles.None, out DateOnly parsedStartDate))
                throw new ArgumentException("Invalid start date format. Use yyyy-MM.");

            if (!DateOnly.TryParseExact(endDate, "yyyy-MM", null, DateTimeStyles.None, out DateOnly parsedEndDate))
                throw new ArgumentException("Invalid end date format. Use yyyy-MM.");

            if (parsedStartDate > parsedEndDate)
                throw new ArgumentException("Start date cannot be after end date.");

            var startOfMonth = parsedStartDate;
            var endOfMonth = parsedEndDate.AddMonths(1).AddDays(-1);

            var startDateTime = startOfMonth.ToDateTime(TimeOnly.MinValue);
            var endDateTime = endOfMonth.ToDateTime(TimeOnly.MinValue);

            var today = DateTime.Today;

            if (garageId.HasValue)
            {
                var garage = _dbContext.Garages
                    .Include(g => g.CarGarages)
                    .FirstOrDefault(g => g.GarageId == garageId.Value);

                if (garage == null)
                    throw new ArgumentException("Garage not found.");

                var todayReport = _dbContext.MaintenanceReports
                    .FirstOrDefault(r => r.YearMonth.Year == today.Year && r.YearMonth.MonthValue == today.Month && r.GarageId == garageId.Value);

                var requestsToday = _dbContext.MaintenanceReports 
                    .Count(r => r.YearMonth.Year == today.Year && r.GarageId == garageId.Value);

                if (todayReport != null)
                {
                    todayReport.GarageId = garageId.Value;
                    todayReport.YearMonth = new YearMonth
                    {
                        Year = today.Year,
                        Month = today.ToString("MMMM").ToUpper(),
                        LeapYear = DateTime.IsLeapYear(today.Year),
                        MonthValue = today.Month
                    };
                    todayReport.Requests = requestsToday;
                    _dbContext.MaintenanceReports.Update(todayReport);
                }
                else
                {
                    var newReport = new MaintenanceReport
                    {
                        GarageId = garageId.Value,
                        YearMonth = new YearMonth
                        {
                            Year = today.Year,
                            Month = today.ToString("MMMM").ToUpper(),
                            LeapYear = DateTime.IsLeapYear(today.Year),
                            MonthValue = today.Month
                        },
                        Requests = requestsToday,
                    };

                    _dbContext.MaintenanceReports.Add(newReport);
                }

                _dbContext.SaveChanges();
            }

            var query = _dbContext.MaintenanceReports.AsQueryable();

            if (garageId.HasValue)
            {
                query = query.Where(r => r.GarageId == garageId.Value);
            }

            query = query.Where(r =>
                r.YearMonth.Year * 100 + r.YearMonth.MonthValue >= startDateTime.Year * 100 + startDateTime.Month &&
                r.YearMonth.Year * 100 + r.YearMonth.MonthValue <= endDateTime.Year * 100 + endDateTime.Month);

            var stats = query.ToList();

            var monthsInRange = Enumerable.Range(0, ((endOfMonth.Year - startOfMonth.Year) * 12) + endOfMonth.Month - startOfMonth.Month + 1)
                .Select(i => startOfMonth.AddMonths(i))
                .ToList();

            var result = monthsInRange.Select(date =>
            {
                var existingReport = stats.FirstOrDefault(r =>
                    r.YearMonth.Year == date.Year && r.YearMonth.MonthValue == date.Month);

                return existingReport ?? new MaintenanceReport
                {
                    GarageId = (int)garageId,
                    YearMonth = new YearMonth
                    {
                        Year = date.Year,
                        Month = date.ToString("MMMM").ToUpper(),
                        LeapYear = DateTime.IsLeapYear(date.Year),
                        MonthValue = date.Month
                    },
                    Requests = 0
                };
            }).ToList();

            return result;
        }


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
