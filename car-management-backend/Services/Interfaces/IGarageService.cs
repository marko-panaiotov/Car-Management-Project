using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Entities;

namespace car_management_backend.Services.Interfaces
{
    public interface IGarageService
    {
        ResponseGarageDto GetGarage(int id);
        IEnumerable<ResponseGarageDto> GetAllGarages(string? city);
        IEnumerable<ResponseGarageDto> GetGaragesByCity(string? city);
        IEnumerable<GarageDailyAvailabilityReportDto> DailyAvailabilityReport(int? garageId, DateTime? startDate, DateTime? endDate);
        void CreateGarage(CreateGarageDto garage);
        void UpdateGarage(int id, UpdateGarageDto garage);
        void DeleteGarage(int id);
    }
}
