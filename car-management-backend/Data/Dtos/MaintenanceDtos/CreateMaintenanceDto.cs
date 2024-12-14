namespace car_management_backend.Data.Dtos.MaintenanceDtos
{
    public class CreateMaintenanceDto
    {
        public int GarageId { get; set; }
        public int CarId { get; set; }
        public string ServiceType { get; set; } = null!;
        public DateTime ScheduledTime { get; set; }
    }
}
