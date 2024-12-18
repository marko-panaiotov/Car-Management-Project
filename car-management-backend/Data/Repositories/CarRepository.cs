using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.ConstrainedExecution;

namespace car_management_backend.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarManagementDbContext _dbContext;
        private static int _apiGarageCallCount = 1;

        public CarRepository(CarManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Car> GetAllCars()
        {
            /* return _dbContext.Cars
                 .AsNoTracking()
                 .Include(c => c.CarGarages)
                 .ToList();*/

            return _dbContext.Cars
                .Include(c => c.CarGarages)
                .ThenInclude(cg => cg.Garage)
                .ToList();
        }

        public IEnumerable<Car> GetCarsByMake(string? make)
        {
            return _dbContext.Cars
                .Include(c => c.CarGarages)
                .ThenInclude(cg => cg.Garage)
                .Where(c => c.Make == make)
                .ToList();
        }

        public IEnumerable<Car> GetCarsByGarageId(int? carByGarageid)
        {
            var garageById = _dbContext.Garages.FirstOrDefault(g => g.GarageId == carByGarageid);
            var today = DateTime.Now.Date;

            var todayReport = _dbContext.GarageReports
                .FirstOrDefault(r => r.GarageId == carByGarageid && r.Date == today);

            if (todayReport != null)
            {

                _apiGarageCallCount = todayReport.Requests + 1;
                todayReport.Requests = _apiGarageCallCount;
                _dbContext.GarageReports.Update(todayReport);
            }
            else
            {
                var newReport = new GarageReport
                {
                    GarageId = garageById.GarageId,
                    Date = today,
                    Requests = 0,
                    AvailableCapacity = garageById.Capacity - garageById.CarGarages.Count
                };

                _dbContext.GarageReports.Add(newReport);
            }

            _dbContext.SaveChanges();

            return _dbContext.Cars
                    .Include(c => c.CarGarages)
                    .ThenInclude(cg => cg.Garage)
                    .Where(c => c.CarGarages.Any(cg => cg.GarageId == carByGarageid))
                    .ToList();
        }

        public IEnumerable<Car> GetCarsFromYearToYear(int? fromYear, int? toYear)
        {
            return _dbContext.Cars              
                .Include(c => c.CarGarages)
                .ThenInclude(cg => cg.Garage)
                .Where(c => 
                        c.ProductionYear >= fromYear.Value && 
                         c.ProductionYear <= toYear.Value 
                )
                .ToList();
        }

        public Car GetCarById(int id)
        {
            return _dbContext.Cars.Include(c => c.CarGarages).FirstOrDefault(c => c.CarId == id);
        }

        public Car AddCar(Car car)
        {
            _dbContext.Cars.Add(car);
            return car;
        }

        public void DeleteCar(int id)
        {
            var carToDelete = _dbContext.Cars
                .FirstOrDefault(c => c.CarId == id);
            _dbContext.Cars.Remove(carToDelete);
        }

        public void UpdateCar(Car car)
        {
            _dbContext.Cars.Update(car);

            _dbContext.SaveChanges();

        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
