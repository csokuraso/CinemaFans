namespace CinemaFans.App.Models;

public sealed class User
{
    public string Login { get; }
    public UserRole Role { get; }

    public User(string login, UserRole role)
    {
        Login = login;
        Role = role;
    }
}
