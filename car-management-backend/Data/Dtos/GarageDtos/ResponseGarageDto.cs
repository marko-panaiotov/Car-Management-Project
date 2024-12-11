namespace car_management_backend.Data.Dtos.GarageDtos
{
    public class ResponseGarageDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public int Capacity { get; set; }
        public string City { get; set; } = null!;
    }
}
