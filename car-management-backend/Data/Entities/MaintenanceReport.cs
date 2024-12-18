namespace car_management_backend.Data.Entities
{
    public class MaintenanceReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public YearMonth YearMonth { get; set; }
        public int Requests { get; set; }
    }
}
