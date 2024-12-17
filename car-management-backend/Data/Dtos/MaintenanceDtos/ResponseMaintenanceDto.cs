using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Entities;
using Newtonsoft.Json;

namespace car_management_backend.Data.Dtos.MaintenanceDtos
{
    public class ResponseMaintenanceDto
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public string CarName { get; set; } = null!;

        public string ServiceType { get; set; } = null!;
        public DateTime ScheduledDate { get; set; }=DateTime.Now;

        public int GarageId { get; set; }

        public string GarageName { get; set; } = null!;


    }
}
