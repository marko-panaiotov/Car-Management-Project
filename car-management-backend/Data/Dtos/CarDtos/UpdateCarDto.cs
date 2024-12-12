using car_management_backend.Data.Entities;
using System.Globalization;

namespace car_management_backend.Data.Dtos.CarDtos
{
    public class UpdateCarDto
    {
        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int ProductionYear { get; set; }

        public string LicensePlate { get; set; } = null!;
        public List<int> GarageIds { get; set; }
    }
}
