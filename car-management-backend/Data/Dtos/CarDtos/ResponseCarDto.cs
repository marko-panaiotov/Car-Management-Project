using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Dtos.CarDtos
{
    public class ResponseCarDto
    {
        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public DateTime ProductionYear { get; set; }

        public string LicensePlate { get; set; } = null!;

        //public ICollection<Garage> Garages { get; set; }
        public ResponseGarageDto Garages { get; set; }
        //public ICollection<CarGarage> Garages { get; set; } = new List<CarGarage>();

    }
}
