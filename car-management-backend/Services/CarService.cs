using car_management_backend.Data.Dtos;
using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Services.Interfaces;
using car_management_backend.Utilities.Helpers;
using System.Data.Common;
using System.IO;

namespace car_management_backend.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepo;

        public CarService(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        public void CreateCar(CreateCarDto carDto)
        {
            var car = new Car
            {
                Make = carDto.Make,
                Model = carDto.Model,
                ProductionYear = carDto.ProductionYear,
                LicensePlate = carDto.LicensePlate,
                CarGarageId = carDto.GarageIds.FirstOrDefault(),
            };
            _carRepo.AddCar(car);
            _carRepo.SaveChanges();
            MapHelper.MapCreateCarToDto(car);
        }

        public IEnumerable<ResponseCarDto> GetAllCars()
        {
            var cars = _carRepo.GetAllCars();
            return cars.Select(c => MapHelper.MapResponseCarToDto(c));
        }

        public ResponseCarDto GetCar(int id)
        {
            var carById = _carRepo.GetCarById(id);
            return MapHelper.MapResponseCarToDto(carById);
        }

        public void UpdateCar(int id, UpdateCarDto carDto)
        {
            var car = _carRepo.GetCarById(id);
            

            if (car != null)
            {
                car.Make = carDto.Make;
                car.Model = carDto.Model;
                car.ProductionYear = carDto.ProductionYear;
                car.LicensePlate = carDto.LicensePlate;
                car.CarGarageId = carDto.GarageIds.FirstOrDefault();
                _carRepo.UpdateCar(car);
                _carRepo.SaveChanges();
                MapHelper.MapUpdateCarToDto(car);
            }
                //_carRepo.UpdateCar(car);

        }

        public void DeleteCar(int id)
        {
            _carRepo.DeleteCar(id);
            _carRepo.SaveChanges();
        }
    }
}
