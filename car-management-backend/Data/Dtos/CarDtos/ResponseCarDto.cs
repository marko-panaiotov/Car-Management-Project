using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Entities;
using System.Globalization;

namespace car_management_backend.Data.Dtos.CarDtos
{
    public class ResponseCarDto
    {
        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int ProductionYear { get; set; }
        public string LicensePlate { get; set; } = null!;

        public List<ResponseGarageDto> Garages { get; set; } = new List<ResponseGarageDto>();
    }
}
