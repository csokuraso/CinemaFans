using CinemaFans.App.Models;
using System.Security.Cryptography;
using System.Text;

namespace CinemaFans.App.Services;

public sealed class AuthService : IAuthService
{
    private readonly Dictionary<string, (string PasswordHash, UserRole Role)> _users = new()
    {
        ["admin"] = (Hash("admin123"), UserRole.Admin),
        ["user"] = (Hash("user123"), UserRole.User)
    };

    public User Login(string login, string password)
    {
        login = login.Trim();
        if (!_users.TryGetValue(login, out var data))
            throw new UnauthorizedAccessException("Користувача не знайдено.");

        if (!CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(data.PasswordHash),
                Encoding.UTF8.GetBytes(Hash(password))))
            throw new UnauthorizedAccessException("Невірний пароль.");

        return new User(login, data.Role);
    }

    private static string Hash(string value)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes);
    }
}
