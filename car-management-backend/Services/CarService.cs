using car_management_backend.Data.Dtos;
using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Services.Interfaces;
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
                CarManufacturer = carDto.Manufacturer,
                CarModel = carDto.Model,
                CarProductionYear = carDto.ProductionYear,
                CarLicensePlate = carDto.LicensePlate,
                GarageIds = carDto.GarageIds
            };
            _carRepo.AddCar(car);
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

        public void UpdateCar(int id, UpdateCarDto carDto)
        {
            var car = _carRepo.GetCarById(id);

            if (car != null)
            {
                car.CarManufacturer = carDto.Manufacturer;
                car.CarModel = carDto.Model;
                car.CarProductionYear = carDto.ProductionYear;
                car.CarLicensePlate = carDto.LicensePlate;
                //car.GarageIds = carDto.GarageIds;
                car.GarageIds = carDto.garage;
                _carRepo.UpdateCar(car);
                _carRepo.SaveChanges();
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
