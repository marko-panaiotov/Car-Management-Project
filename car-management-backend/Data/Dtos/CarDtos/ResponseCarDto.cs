using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Dtos.CarDtos
{
    public class ResponseCarDto
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; } = null!;

        public string Model { get; set; } = null!;

        public DateTime ProductionYear { get; set; }

        public string LicensePlate { get; set; } = null!;

        public virtual ICollection<Garage> Garages { get; set; }

    }
}
