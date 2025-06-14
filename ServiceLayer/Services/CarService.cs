// Services/CarService.cs
using Zaliczenie.Models;

public class CarService
{
    private readonly List<Car> _cars = new()
    {
        Car.Create("Audi", "A4", 200),
        Car.Create("Volvo", "S60", 254)
    };

    public List<Car> GetAll() => _cars;

    public Car? GetById(Guid id) => _cars.FirstOrDefault(c => c.Id == id);

    public Car CreateCar(string made, string model, int horsePower)
    {
        var newCar = Car.Create(made, model, horsePower);
        _cars.Add(newCar);
        return newCar;
    }

    public void UpdateCar(Guid id, int newHorsePower)
    {
        var car = _cars.FirstOrDefault(x => x.Id == id);

        car.ChangeHorsePower(newHorsePower);

        
    }
}
