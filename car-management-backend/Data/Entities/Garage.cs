using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_management_backend.Data.Entities
{
    public class Garage
    {
        public int GarageId { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public int Capacity { get; set; }
        public string City { get; set; } = null!;
        public ICollection<CarGarage> CarGarages { get; set; } = new List<CarGarage>();

    }
}

