using car_management_backend.Data.Dtos;
using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Entities;
using car_management_backend.Data.Repositories.Interfaces;
using car_management_backend.Services.Interfaces;
using car_management_backend.Utilities.Helpers;
using System.Data.Common;
using System.Globalization;
using System.IO;

namespace car_management_backend.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepo;
        private readonly IGarageRepository _garageRepo;

        public CarService(ICarRepository carRepo, IGarageRepository garageRepo)
        {
            _carRepo = carRepo;
            _garageRepo = garageRepo;
        }

        public void CreateCar(CreateCarDto carDto)
        {
            var car = new Car
            {
                Make = carDto.Make,
                Model = carDto.Model,
                ProductionYear = carDto.ProductionYear,
                LicensePlate = carDto.LicensePlate,
                CarGarages = carDto.GarageIds
                            .Select(garageId => new CarGarage { GarageId = garageId })
                            .ToList(),
            };

            _carRepo.AddCar(car);
            _carRepo.SaveChanges();
            MapHelper.MapCreateCarToDto(car);
        }

        public IEnumerable<ResponseCarDto> GetAllCars(string? carMake, int? garageId, int? fromYear, int? toYear)
        {
            if (carMake != null || garageId != 0 || fromYear != 0 && toYear != 0)
             {
                if (carMake != null)
                {
                    return GetCarsByMake(carMake);
                }
                if (garageId != 0)
                {
                    return GetCarsByGarageId(garageId);
                }
                if (fromYear != 0 && toYear != 0)
                {
                    return GetCarsFromYearToYear(fromYear, toYear);
                }
            }
            var cars = _carRepo.GetAllCars();
            return cars.Select(c => MapHelper.MapResponseCarToDto(c));
        }

        public IEnumerable<ResponseCarDto> GetCarsByMake(string? make)
        {
            var cars = _carRepo.GetCarsByMake(make);
            return cars.Select(c => MapHelper.MapResponseCarToDto(c));
        }

        public IEnumerable<ResponseCarDto> GetCarsByGarageId(int? carByGarageId)
        {
            var cars = _carRepo.GetCarsByGarageId(carByGarageId);
            return cars.Select(c => MapHelper.MapResponseCarToDto(c));
        }

        public IEnumerable<ResponseCarDto> GetCarsFromYearToYear(int? fromYear, int? toYear)
        {
            var cars = _carRepo.GetCarsFromYearToYear(fromYear,toYear);
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

            car.Make = carDto.Make;
            car.Model = carDto.Model;
            car.ProductionYear = carDto.ProductionYear;
            car.LicensePlate = carDto.LicensePlate;

            foreach (var garageId in carDto.GarageIds)
            {
                var garage = _garageRepo.GetGarageById(garageId);
                var carGarageToRemove = car.CarGarages.FirstOrDefault(cg => cg.GarageId == garageId);

                if (garage != null)
                {
                    if (!car.CarGarages.Any(cg => cg.GarageId == garageId))
                    {
                        car.CarGarages.Add(new CarGarage
                        {
                            CarId = car.CarId,
                            GarageId = garageId
                        });
                    }
                    if (carDto.GarageIds.Count() > 1)
                    {
                        
                        car.CarGarages.Remove(carGarageToRemove);
                    }
                    if(carDto.GarageIds.Count() == 1)
                    {
                        car.CarGarages.FirstOrDefault(cg => cg.GarageId == garageId);
                    }
                }

            }
            _carRepo.UpdateCar(car);
            _carRepo.SaveChanges();

            MapHelper.MapUpdateCarToDto(car);

        }

        public void DeleteCar(int id)
        {
            _carRepo.DeleteCar(id);
            _carRepo.SaveChanges();
        }
    }
}
