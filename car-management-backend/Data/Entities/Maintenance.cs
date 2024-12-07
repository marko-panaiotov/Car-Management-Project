using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_management_backend.Data.Entities
{
    public class Maintenance
    {
        public int MaintenanceId { get; set; }

        public int MaintenanceCarId { get; set; }
        public Car Car { get; set; } = null!;

        public string MaintenanceCarName { get; set; } = null!;

        public string MaintenanceServiceType { get; set; } = null!;

        public DateTime MaintenanceScheduledTime { get; set; }

        public int MaintenanceGarageId { get; set; }
        public Garage Garage { get; set; } = null!;

        public string MaintenaceGarageName { get; set; } = null!;
    }
}

