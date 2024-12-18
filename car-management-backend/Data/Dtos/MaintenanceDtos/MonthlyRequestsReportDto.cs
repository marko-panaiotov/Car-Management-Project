using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Dtos.MaintenanceDtos
{
    public class MonthlyRequestsReportDto
    {
        public YearMonth YearMonth {  get; set; }
        public int Requests { get; set; }
    }
}
