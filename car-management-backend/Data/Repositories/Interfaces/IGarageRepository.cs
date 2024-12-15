using car_management_backend.Data.Entities;

namespace car_management_backend.Data.Repositories.Interfaces
{
    public interface IGarageRepository
    {
        IEnumerable<Garage> GetAllGarages();
        IEnumerable<Garage> GetGaragesByCity(string? city);
        IEnumerable<GarageReport> DailyAvailabilityReport(int? garageId, DateTime? startDate, DateTime? endDate);
        Garage GetGarageById(int id);
        Garage AddGarage(Garage garage);
        void UpdateGarage(Garage garage);
        void DeleteGarage(int id);
        void SaveChanges();
    }
}
