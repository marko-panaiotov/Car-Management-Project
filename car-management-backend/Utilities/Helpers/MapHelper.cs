using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Entities;

namespace car_management_backend.Utilities.Helpers
{
    public class MapHelper
    {
        public static CreateCarDto MapCreateCarToDto(Car car)
        {
            return new CreateCarDto()
            {
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                GarageIds = new List<int> { car.CarGarageId }
            };
        }

        public static UpdateCarDto MapUpdateCarToDto(Car car)
        {
            return new UpdateCarDto()
            {
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                GarageIds = new List<int> { car.CarGarageId }
            };
        }

        public static ResponseCarDto MapResponseCarToDto(Car car)
        {
            return new ResponseCarDto()
            {
                Id = car.CarId,
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                Garages = new ResponseGarageDto()
                { 
                
                    Id = car.Garage.GarageId,
                    Name = car.Garage.Name,
                    Location = car.Garage.Location,
                    City = car.Garage.City,
                    Capacity = car.Garage.Capacity
                }
            };
        }

        public static CreateGarageDto MapCreateGarageToDto(Garage garage)
        {
            return new CreateGarageDto()
            {
                Name = garage.Name,
                Location = garage.Location,
                City = garage.City,
                Capacity = garage.Capacity
            };
        }

        public static UpdateGarageDto MapUpdateGarageToDto(Garage garage)
        {
            return new UpdateGarageDto()
            {
                Name = garage.Name,
                Location = garage.Location,
                City = garage.City,
                Capacity = garage.Capacity
            };
        }

        public static ResponseGarageDto MapResponseGarageToDto(Garage garage)
        {
            return new ResponseGarageDto()
            {
                Id = garage.GarageId,
                Name = garage.Name,
                Location = garage.Location,
                City = garage.City,
                Capacity = garage.Capacity
            };
        }

    }
}
