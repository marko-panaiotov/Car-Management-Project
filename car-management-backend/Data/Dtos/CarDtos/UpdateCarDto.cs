using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Dtos.CarDtos
{
    public class UpdateCarDto
    {
        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public DateTime ProductionYear { get; set; }

        public string LicensePlate { get; set; } = null!;
        public ICollection<int> GarageIds { get; set; }
    }
}
