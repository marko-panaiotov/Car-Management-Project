using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Entities;
using car_management_backend.Services.Interfaces;
using car_management_backend.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace car_management_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class carsController : ControllerBase
    {
        private readonly ICarService _carService;

        public carsController(ICarService carService)
        {
            _carService = carService;
        }


        [HttpGet]
        public List<ResponseCarDto> GetAllCars()
        {
            var result = _carService.GetAllCars();
            return result
           .Select(c => MapHelper.MapResponseCarToDto(c))
           .ToList();
        }

        [HttpGet("{id}")]
        public Car GetCar([FromQuery] int id)
        {
            return _carService.GetCar(id);
        }

        [HttpPost]
        public void AddNewCar([FromBody] CreateCarDto car)
        {
            _carService.CreateCar(car);
        }

        [HttpPut("{id}")]
        public void UpdateCar([FromQuery] int id, [FromBody] UpdateCarDto car)
        {
            _carService.UpdateCar(id,car);
        }

        [HttpDelete("{id}")]
        public void DeleteCar([FromQuery] int id)
        {
            _carService.DeleteCar(id);
        }
    }
}
