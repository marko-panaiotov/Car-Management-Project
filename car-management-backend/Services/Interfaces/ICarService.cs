using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace car_management_backend.Services.Interfaces
{
    public interface ICarService
    {
        ResponseCarDto GetCar(int id);
        IEnumerable<ResponseCarDto> GetAllCars(string? carMake, int? garageId, int? fromYear, int? toYear);
        IEnumerable<ResponseCarDto> GetCarsByMake(string? make);
        IEnumerable<ResponseCarDto> GetCarsByGarageId(int? carByGarageId);
        IEnumerable<ResponseCarDto> GetCarsFromYearToYear(int? fromYear, int? toYear);
        void CreateCar(CreateCarDto car);
        void UpdateCar(int id, UpdateCarDto car);
        void DeleteCar(int id);
    }
}
