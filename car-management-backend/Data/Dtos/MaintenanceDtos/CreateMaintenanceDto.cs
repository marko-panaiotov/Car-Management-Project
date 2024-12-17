using car_management_backend.Data.Dtos.GarageDtos;
using Newtonsoft.Json;

namespace car_management_backend.Data.Dtos.MaintenanceDtos
{
    public class CreateMaintenanceDto
    {
        public int GarageId { get; set; }
        public int CarId { get; set; }
        public string ServiceType { get; set; } = null!;
        public DateTime ScheduledDate { get; set; }
    }
}
