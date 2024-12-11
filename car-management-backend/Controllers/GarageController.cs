using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Services;
using car_management_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace car_management_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class garagesController : ControllerBase
    {

        private readonly IGarageService _garageService;

        public garagesController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        [HttpGet]
        public IActionResult GetAllGarages()
        {
            var result = _garageService.GetAllGarages();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetGarage(int id)
        {
            var car = _garageService.GetGarage(id);
            return Ok(car);
        }

        [HttpPost]
        public IActionResult AddNewGarage([FromBody] CreateGarageDto garageDto)
        {
            _garageService.CreateGarage(garageDto);
            return Ok(garageDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGarage(int id, [FromBody] UpdateGarageDto garageDto)
        {
            _garageService.UpdateGarage(id, garageDto);
            return Ok(garageDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGarage(int id)
        {
            _garageService.DeleteGarage(id);
            return Ok(id);
        }


    }
}
