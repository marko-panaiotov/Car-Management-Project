using car_management_backend.Data.Dtos.GarageDtos;

namespace car_management_backend.Services.Interfaces
{
    public interface IGarageService
    {
        ResponseGarageDto GetGarage(int id);
        IEnumerable<ResponseGarageDto> GetAllGarages();
        void CreateGarage(CreateGarageDto garage);
        void UpdateGarage(int id, UpdateGarageDto garage);
        void DeleteGarage(int id);
    }
}
