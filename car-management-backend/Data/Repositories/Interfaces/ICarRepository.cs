using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Repositories.Interfaces
{
    public interface ICarRepository
    {
        List<Car> GetAllCars();
        Car GetCarById(int id);
        Car AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
        void SaveChanges();
    }
}
