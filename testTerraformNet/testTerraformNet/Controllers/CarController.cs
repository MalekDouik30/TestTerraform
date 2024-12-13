using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using testTerraformNet.Entites;
using testTerraformNet.Service;

namespace testTerraformNet.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(Guid id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }

            var createdCar = await _carService.CreateCarAsync(car);
            return CreatedAtAction(nameof(GetCarById), new { id = createdCar.Id }, createdCar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(Guid id, [FromBody] Car updatedCar)
        {
            if (updatedCar == null || id == Guid.Empty)
            {
                return BadRequest();
            }

            var car = await _carService.UpdateCarAsync(id, updatedCar);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            var isDeleted = await _carService.DeleteCarAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
