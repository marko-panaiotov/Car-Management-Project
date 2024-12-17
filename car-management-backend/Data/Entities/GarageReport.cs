namespace car_management_backend.Data.Entities
{
    public class GarageReport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Requests { get; set; }
        public int AvailableCapacity { get; set; }
        public int GarageId { get; set; }
        public Garage Garage { get; set; }
    }
}
