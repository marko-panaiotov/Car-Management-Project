using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_management_backend.Data.Entities
{
    public class Garage
    {
        public int GarageId { get; set; }

        public string GarageName { get; set; } = null!;

        public string GarageLocation { get; set; } = null!;

        public string GarageCity { get; set; } = null!;

        public int GarageCapacity { get; set; }

    }
}

