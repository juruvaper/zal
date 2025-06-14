namespace Zaliczenie.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }


        public User(string username, string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Username = username;

        }

        public static User Create(string username, string name)
        {
            return new User(username, name);
        }
    }
}