namespace car_management_backend.Data.Entities
{
    public class CarGarage
    {
        public int CarId { get; set; }
        public Car Car { get; set; } = null!;

        public int GarageId { get; set; }
        public Garage Garage { get; set; } = null!;
    }
}
