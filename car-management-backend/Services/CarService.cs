using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Services.Interfaces;

namespace car_management_backend.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepo;

        public CarService(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        public void CreateCar(Car car)
        {
            _carRepo.AddCar(car);
            _carRepo.SaveChanges();
        }

        public void DeleteCar(int id)
        {
            _carRepo.DeleteCar(id);
            _carRepo.SaveChanges();
        }

        public List<Car> GetAllCars()
        {
            return _carRepo.GetAllCars();
        }

        public Car GetCar(int id)
        {
            return _carRepo.GetCarById(id);
        }

        public void UpdateCar(Car car)
        {
            _carRepo.UpdateCar(car);
            _carRepo.SaveChanges();
        }
    }
}
