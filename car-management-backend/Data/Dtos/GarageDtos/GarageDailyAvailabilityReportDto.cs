using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

//using System.Text.Json;
//using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace car_management_backend.Data.Dtos.GarageDtos
{
    public class GarageDailyAvailabilityReportDto
    {
        public DateTime Date {  get; set; }
        public int Requests { get; set; }
        public int AvailableCapacity { get; set; }
    }
}