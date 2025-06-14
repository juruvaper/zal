// Services/UserService.cs
using Zaliczenie.Models;

public class UserService
{


    private readonly List<User> _users = new()
    {
        User.Create("anna123", "Anna"),
        User.Create("maro123", "Marek")
    };

    private int _nextId = 3;

    public List<User> GetAll() => _users;

    public User? GetById(Guid id) => _users.FirstOrDefault(u => u.Id == id);

    public User CreateUser(string username, string name)
    {
        var newUser = User.Create(username, name);
        _users.Add(newUser);
        return newUser;
    }
}
