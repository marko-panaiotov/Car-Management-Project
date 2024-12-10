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
                Garages = new List<int> { car.CarGarageId }
            };
        }

    }
}
