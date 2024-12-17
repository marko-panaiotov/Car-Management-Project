using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace car_management_backend.Data.Repositories
{
    public class GarageRepository : IGarageRepository
    {
        private readonly CarManagementDbContext _dbContext;
        private static int _apiGarageCallCount = 0;

        public GarageRepository(CarManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Garage> GetAllGarages()
        {
            return _dbContext.Garages
                .Include(c => c.CarGarages)
                .ToList();
        }

        public IEnumerable<Garage> GetGaragesByCity(string? city)
        {
            return _dbContext.Garages
              .Include(g => g.CarGarages)
              .Where(g => g.City == city)
              .ToList();
        }

        public Garage GetGarageById(int id)
        {
            var garageById = _dbContext.Garages.FirstOrDefault(g => g.GarageId == id);
            var today = DateTime.Now.Date;

            if (garageById == null)
            {
                return null;
            }

            var todayReport = _dbContext.GarageReports
                .FirstOrDefault(r => r.GarageId == id && r.Date == today);

            if (todayReport != null)
            {
                _apiGarageCallCount = todayReport.Requests + 1; // Increment the requests
               // _apiGarageCallCount++;
                todayReport.Requests=_apiGarageCallCount;
                _dbContext.GarageReports.Update(todayReport);
                _apiGarageCallCount = 0;
            }
            else
            {
                var newReport = new GarageReport
                {
                    GarageId = id,
                    Date = today,
                    Requests = 0,
                    AvailableCapacity = garageById.Capacity - garageById.CarGarages.Count
                };

                _dbContext.GarageReports.Add(newReport);
            }

            _dbContext.SaveChanges();

            return garageById;

        }
        public Garage AddGarage(Garage garage)
        {
            _dbContext.Garages.Add(garage);
            return garage;
        }
        public void UpdateGarage(Garage garage)
        {
            _dbContext.Garages.Update(garage);
        }
        public void DeleteGarage(int id)
        {
            var garageRequest = _dbContext.GarageReports.FirstOrDefault(g => g.Id == id);
            var garageToDelete = _dbContext.Garages
              .FirstOrDefault(c => c.GarageId == id);

            _dbContext.Garages.Remove(garageToDelete);
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<GarageReport> DailyAvailabilityReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
                throw new ArgumentException("Start date and end date must be provided.");

            var today = DateTime.Now.Date;

            if (garageId.HasValue)
            {
                var garage = _dbContext.Garages
                    .Include(g => g.CarGarages)
                    .FirstOrDefault(g => g.GarageId == garageId.Value);

                if (garage == null)
                    throw new ArgumentException("Garage not found.");

                var todayReport = _dbContext.GarageReports
                    .FirstOrDefault(r => r.GarageId == garageId.Value && r.Date == today);

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
                        Requests = _apiGarageCallCount,
                        AvailableCapacity = availableCapacity
                    };

                    _dbContext.GarageReports.Add(newReport);
                }

                _dbContext.SaveChanges();
            }

            var query = _dbContext.GarageReports
                .Where(r => r.Date >= startDate.Value && r.Date <= endDate.Value);

            if (garageId.HasValue)
            {
                query = query.Where(r => r.GarageId == garageId.Value);
            }

            var garageData = _dbContext.Garages
                .Include(g => g.CarGarages)
                .ToDictionary(g => g.GarageId, g => new { g.Capacity, CarCount = g.CarGarages.Count });

            var stats = query.Select(r => new GarageReport
            {
                GarageId = r.GarageId,
                Date = r.Date,
                Requests = r.Requests,
                AvailableCapacity = (r.Date == today && garageId.HasValue && r.GarageId == garageId.Value)
                    ? garageData[r.GarageId].Capacity - garageData[r.GarageId].CarCount
                    : r.AvailableCapacity
            }).ToList();

            return stats;

        }
    }
}
