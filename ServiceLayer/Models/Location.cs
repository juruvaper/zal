namespace Zaliczenie.Models
{
    public class Location
    {
        public Location(string city)
        {
            Id = Guid.NewGuid();
            City = city;
        }

        public Guid Id { get; set; }
        public string City { get; set; }

        public static Location Create(string city)
        {
            return new Location(city);
        }
    }
}
