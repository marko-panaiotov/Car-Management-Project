using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace car_management_backend.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarManagementDbContext _dbContext;

        public CarRepository(CarManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Car> GetAllCars()
        {
            return _dbContext.Cars.ToList();
                /*.AsNoTracking()
                .Include(c => c.CarGarageId)
                .ToList();*/
        }

        public Car GetCarById(int id)
        {
            return _dbContext.Cars.Find(id);
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
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
