using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Entities;

namespace car_management_backend.Services.Interfaces
{
    public interface ICarService
    {
        ResponseCarDto GetCar(int id);
        IEnumerable<ResponseCarDto> GetAllCars();
        void CreateCar(CreateCarDto car);
        void UpdateCar(int id, UpdateCarDto car);
        void DeleteCar(int id);
    }
}
