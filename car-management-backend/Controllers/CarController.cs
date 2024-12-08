﻿using car_management_backend.Data.Dtos.CarDtos;
using car_management_backend.Data.Entities;
using car_management_backend.Services.Interfaces;
using car_management_backend.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace car_management_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }


        [HttpGet("GetAllCars")]
        public List<ResponseCarDto> GetAllCars()
        {
            var result = _carService.GetAllCars();
            return result
           .Select(c => MapHelper.MapResponseCarToDto(c))
           .ToList();
        }

        [HttpGet("GetCarById")]
        public Car GetCar(int carId)
        {
            return _carService.GetCar(carId);
        }

        [HttpPost("AddNewCar")]
        public void AddNewCar([FromBody] CreateCarDto car)
        {
            _carService.CreateCar(car);
        }

        [HttpPut("EditCarById")]
        public void UpdateCar([FromQuery] int id, [FromBody] UpdateCarDto car)
        {
            _carService.UpdateCar(id,car);
        }

        [HttpDelete("DeleteCarById")]
        public void DeleteCar(int carId)
        {
            _carService.DeleteCar(carId);
        }
    }
}
