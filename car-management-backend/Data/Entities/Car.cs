using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace car_management_backend.Data.Entities
{
    public class Car
    {
        public int CarId { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string ProductionYear { get; set; }

        public string LicensePlate { get; set; } = null!;

        public int CarGarageId { get; set; }
        public Garage Garage { get; set; } = null!;

    }
}

