using car_management_backend.Data.Dtos.GarageDtos;
using car_management_backend.Data.Dtos.MaintenanceDtos;
using car_management_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace car_management_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class maintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public maintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Resource found")]
        [SwaggerResponse(400, "Bad request")]

        public async Task<ActionResult<ResponseMaintenanceDto>> GetAllMaintenaces([FromQuery] int? carId, [FromQuery] int? garageId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var result = _maintenanceService.GetAllMaintenaces(carId, garageId, startDate, endDate);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Resource found")]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult<ResponseMaintenanceDto>> GetMaintenaceById(int id)
        {
            var car = _maintenanceService.GetMaintenaceById(id);
            return Ok(car);
        }

        [HttpPost]
        [SwaggerResponse(200, "Resource created")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<ActionResult<ResponseMaintenanceDto>> AddNewMaintenace([FromBody] CreateMaintenanceDto garageDto)
        {
            _maintenanceService.AddNewMaintenace(garageDto);
            return Ok(garageDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Resource updated")]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult<ResponseMaintenanceDto>> UpdateMaintenace(int id, [FromBody] UpdateMaintenanceDto maintenanceDto)
        {
            _maintenanceService.UpdateMaintenace(id, maintenanceDto);
            return Ok(maintenanceDto);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Resource deleted")]
        [SwaggerResponse(400, "Bad request")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult<bool>> DeleteMaintenace(int id)
        {
            _maintenanceService.DeleteMaintenace(id);
            return Ok(true);
        }

        [HttpGet("monthlyRequestsReport")]
        [SwaggerResponse(200, "Resource created")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<ActionResult<MonthlyRequestsReportDto>> MonthlyRequestsReport([FromQuery] int? garageId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            _maintenanceService.MonthlyRequestsReport(garageId, startDate, endDate);
            return Ok(true);
        }


    }
}
