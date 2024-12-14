using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Dtos.MaintenanceDtos
{
    public class UpdateMaintenanceDto
    {
        public int CarId { get; set; }
        public string ServiceType { get; set; } = null!;
        public DateTime ScheduledTime { get; set; }
        public int GarageId { get; set; }
    }
}
