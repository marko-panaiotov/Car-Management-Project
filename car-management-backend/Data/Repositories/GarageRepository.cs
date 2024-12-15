using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace car_management_backend.Data.Repositories
{
    public class GarageRepository : IGarageRepository
    {
        private readonly CarManagementDbContext _dbContext;

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
            return _dbContext.Garages.Find(id);
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

         /*   return _dbContext.GarageReports
                .Include(c => c.Garage)
                .Where(g => g.GarageId == garageId &&
                        g.Date >= startDate.Value &&
                         g.Date <= endDate.Value)
                .ToList();*/


                var stats = _dbContext.GarageReports
                .Where(r => r.Date >= startDate && r.Date <= endDate)
                .GroupBy(r => new { r.Date, r.GarageId })
                .Select(g => new GarageReport
                 {
                     GarageId = g.Key.GarageId,
                     Date = g.Key.Date,
                     AvailableCapacity = _dbContext.GarageReports
                     .Where(s => s.Id == g.Key.GarageId)
                     .FirstOrDefault().AvailableCapacity - g.Sum(r => r.Requests)
                     })
                    .ToList();

            return stats;
            //throw new NotImplementedException();

        }




    }
}
