using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Services;
using car_management_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

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
        [SwaggerResponse(200, "Resource found")]
        [SwaggerResponse(400, "Bad request")]

        public async Task<ActionResult<ResponseGarageDto>> GetAllGarages([FromQuery] string? city)
        {
            var result = _garageService.GetAllGarages(city);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Resource found")]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult<ResponseGarageDto>> GetGarage(int id)
        {
            var car = _garageService.GetGarage(id);
            return Ok(car);
        }

        [HttpPost]
        [SwaggerResponse(200, "Resource created")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<ActionResult<ResponseGarageDto>> AddNewGarage([FromBody][Required] CreateGarageDto garageDto)
        {
            _garageService.CreateGarage(garageDto);
            return Ok(garageDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Resource updated")]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult<ResponseGarageDto>> UpdateGarage(int id, [FromBody][Required] UpdateGarageDto garageDto)
        {
            _garageService.UpdateGarage(id, garageDto);
            return Ok(garageDto);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Resource deleted")]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult<bool>> DeleteGarage( int id)
        {
            _garageService.DeleteGarage(id);
            return Ok(true);
        }

        [HttpGet("dailyAvailabilityReport")]
        [SwaggerResponse(200, "Resource created")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<ActionResult<GarageDailyAvailabilityReportDto>> DailyAvailabilityReport([FromQuery][Required] int garageId, [FromQuery][Required] DateTime startDate, [FromQuery][Required] DateTime endDate)
        {
            var report = _garageService.DailyAvailabilityReport(garageId, startDate, endDate);
            return Ok(report);
        }


    }
}
