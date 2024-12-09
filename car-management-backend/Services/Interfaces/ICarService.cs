using car_management_backend.Data.Entities;

namespace car_management_backend.Services.Interfaces
{
    public interface ICarService
    {
        Car GetCar(int id);
        List<Car> GetAllCars();
        void CreateCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
    }
}
