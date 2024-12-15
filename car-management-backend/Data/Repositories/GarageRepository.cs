using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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
            _apiGarageCallCount++;
            return _dbContext.Garages
                .Include(c => c.CarGarages)
                .ToList();
        }

        public IEnumerable<Garage> GetGaragesByCity(string? city)
        {
            _apiGarageCallCount++;
            return _dbContext.Garages
              .Include(g => g.CarGarages)
              .Where(g => g.City == city)
              .ToList();
        }

        public Garage GetGarageById(int id)
        {
            _apiGarageCallCount++;
            return _dbContext.Garages.Find(id);
        }
        public Garage AddGarage(Garage garage)
        {
            _apiGarageCallCount++;
            _dbContext.Garages.Add(garage);
            return garage;
        }
        public void UpdateGarage(Garage garage)
        {
            _apiGarageCallCount++;
            _dbContext.Garages.Update(garage);
        }
        public void DeleteGarage(int id)
        {
            _apiGarageCallCount++;
            var garageToDelete = _dbContext.Garages
                .FirstOrDefault(c => c.GarageId == id);
            _dbContext.Garages.Remove(garageToDelete);
        }
        public void SaveChanges()
        {
            _apiGarageCallCount++;
            _dbContext.SaveChanges();
        }

        public IEnumerable<GarageReport> DailyAvailabilityReport(int? garageId, DateTime? startDate, DateTime? endDate)
        {
            _apiGarageCallCount++;

            if (!startDate.HasValue || !endDate.HasValue)
                throw new ArgumentException("Start date and end date must be provided.");

            // Build the query for fetching the daily availability reports
            var query = _dbContext.GarageReports
                .Where(r => r.Date >= startDate.Value && r.Date <= endDate.Value);

            // If a specific garageId is provided, filter by it
            if (garageId.HasValue)
            {
                query = query.Where(r => r.GarageId == garageId.Value);
            }

            // Fetch the garage data to calculate available capacity
            var garageData = _dbContext.Garages
                .Include(g => g.CarGarages) // Ensure CarGarages are loaded
                .ToDictionary(g => g.GarageId, g => new { g.Capacity, CarCount = g.CarGarages.Count() });

            // Now fetch the data for each report
            var stats = query.Select(r => new GarageReport
            {
                GarageId = r.GarageId,
                Date = r.Date,
                AvailableCapacity = garageData.ContainsKey(r.GarageId)
                    ? garageData[r.GarageId].Capacity - garageData[r.GarageId].CarCount // Total capacity minus cars in the garage
                    : 0 // Default to 0 if garage not found
            })
            .ToList();

            // Add a new daily availability report if necessary
            if (garageId.HasValue)
            {
                var newReport = new GarageReport
                {
                    GarageId = garageId.Value,
                    Date = DateTime.Now,
                    Requests = _apiGarageCallCount, // Initialize with 0
                    AvailableCapacity = garageData.ContainsKey(garageId.Value)
                        ? garageData[garageId.Value].Capacity - garageData[garageId.Value].CarCount
                        : 0
                };

                _dbContext.GarageReports.Add(newReport);
                _dbContext.SaveChanges();
            }

            return stats;



            //throw new NotImplementedException();

        }
    }
}
