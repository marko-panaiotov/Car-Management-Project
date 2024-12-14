using System.Text.Json;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace car_management_backend.Data.Dtos.GarageDtos
{
    public class GarageDailyAvailabilityReportDto
    {
        public DateTime date { get; set; } = DateTime.Now;
        public int requests { get; set; }
        public int availableCapacity { get; set; }


    }

  
}