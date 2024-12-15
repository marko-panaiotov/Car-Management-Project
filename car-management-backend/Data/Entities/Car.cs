using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;

namespace car_management_backend.Data.Entities
{
    public class Car
    {
        public int CarId { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;
        public int ProductionYear {  get; set; }
     
        public string LicensePlate { get; set; } = null!;
        public ICollection<CarGarage> CarGarages { get; set; } = new List<CarGarage>();
        public ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    }
}

