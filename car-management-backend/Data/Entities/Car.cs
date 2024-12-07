using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_management_backend.Data.Entities
{
    public class Car
    {
        public int CarId { get; set; }

        public string CarManufacturer { get; set; } = null!;

        public string CarModel { get; set; } = null!;

        public DateTime CarProductionYear { get; set; }

        public string CarLicensePlate { get; set; } = null!;

        public int CarGarageId { get; set; }
        public Garage Garage { get; set; } = null!;
    }
}

