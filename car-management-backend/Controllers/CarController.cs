using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllCars()
        {
            var result = _carService.GetAllCars();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            var car = _carService.GetCar(id);
            return Ok(car);
        }

        [HttpPost]
        public IActionResult AddNewCar([FromBody] CreateCarDto car)
        {
            _carService.CreateCar(car);
            return Ok(car);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] UpdateCarDto car)
        {
            _carService.UpdateCar(id,car);
            return Ok(car);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            _carService.DeleteCar(id);
            return Ok(id);
        }
    }
}
