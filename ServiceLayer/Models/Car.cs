namespace Zaliczenie.Models
{
    public class Car
    {

        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int HorsePower { get; set; }

        public Car(string brand, string model, int horsePower)
        {
            Id = Guid.NewGuid();
            Brand = brand;
            Model = model;
            HorsePower = horsePower;
        }

        public void ChangeHorsePower(int newHorsePower)
        {
            HorsePower = newHorsePower;
        }

        public static Car Create(string brand, string model, int horsePower)
        {
            return new Car(brand, model, horsePower);
        }

    }
}
