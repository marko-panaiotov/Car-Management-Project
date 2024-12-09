using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Entities;

namespace car_management_backend.Utilities.Helpers
{
    public class MapHelper
    {
        public static CreateCarDto MapCreateCarToDto(Car car)
        {
            return new CreateCarDto()
            {
                Manufacturer = car.CarManufacturer,
                Model = car.CarModel,
                ProductionYear = car.CarProductionYear,
                LicensePlate = car.CarLicensePlate,
                GarageIds = car.GarageIds
            };
        }

        public static UpdateCarDto MapUpdateCarToDto(Car car)
        {
            return new UpdateCarDto()
            {
                Manufacturer = car.CarManufacturer,
                Model = car.CarModel,
                ProductionYear = car.CarProductionYear,
                LicensePlate = car.CarLicensePlate,
                GarageIds = car.GarageIds,
                //garage = car.CarGarageId
            };
        }

        public static ResponseCarDto MapResponseCarToDto(Car car)
        {
            return new ResponseCarDto()
            {
                Id = car.CarId,
                Manufacturer = car.CarManufacturer,
                Model = car.CarModel,
                ProductionYear = car.CarProductionYear,
                LicensePlate = car.CarLicensePlate,
                Garages = car.GarageIds
            };
        }

    }
}
