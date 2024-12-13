using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Repositories.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars();
        IEnumerable<Car> GetCarsByMake(string? make);
        IEnumerable<Car> GetCarsByGarageId(int? carByGarageid);
        IEnumerable<Car> GetCarsFromYearToYear(int? fromYear, int? toYear);
        Car GetCarById(int id);
        Car AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
        void SaveChanges();
    }
}
