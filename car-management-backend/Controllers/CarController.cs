using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Services.Interfaces;
using car_management_backend.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerResponse(200, "Resource found")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<ActionResult<ResponseCarDto>> GetAllCars()
        {
            var result = _carService.GetAllCars();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Resource found")]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult<ResponseCarDto>> GetCar(int id)
        {
            var car = _carService.GetCar(id);
            return Ok(car);
        }

        [HttpPost]
        [SwaggerResponse(200, "Resource created")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<ActionResult<ResponseCarDto>> AddNewCar([FromBody] CreateCarDto car)
        {
            _carService.CreateCar(car);
            return Ok(car);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Resource updated")]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult<ResponseCarDto>> UpdateCar(int id, [FromBody] UpdateCarDto car)
        {
            _carService.UpdateCar(id,car);
            return Ok(car);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Resource deleted")]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult<ResponseCarDto>> DeleteCar(int id)
        {
            _carService.DeleteCar(id);
            return Ok(id);
        }
    }
}
