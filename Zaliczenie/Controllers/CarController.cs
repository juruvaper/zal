// Controllers/CarController.cs
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using Zaliczenie.Models;


[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly CarService _carService;

    public CarController(CarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_carService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var car = _carService.GetById(id);
        if (car is null)
            return NotFound();

        return Ok(car);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateCarDTO request)
    {
        var car = _carService.CreateCar(request.brand, request.model, request.horsePower);
        return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateHorsePower([FromBody] Guid id, int newHorsePower)
    {
        var car = _carService.GetById(id);
        if (car is null)
            return NotFound();

        _carService.UpdateCar(id, newHorsePower);
        return NoContent();
    }
}
