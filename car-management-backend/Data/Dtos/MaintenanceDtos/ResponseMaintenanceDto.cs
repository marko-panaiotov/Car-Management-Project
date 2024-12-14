using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Dtos.MaintenanceDtos
{
    public class ResponseMaintenanceDto
    {
        public int Id { get; set; }

        public int CarId { get; set; }
       // public Car Car { get; set; } = null!;

        public string CarName { get; set; } = null!;

        public string ServiceType { get; set; } = null!;

        public DateTime ScheduledTime { get; set; }

        public int GarageId { get; set; }
       // public Garage Garage { get; set; } = null!;

        public string GarageName { get; set; } = null!;


    }
}
