using CinemaFans.App.Models;

namespace CinemaFans.App.Services;

public interface IAuthService
{
    User Login(string login, string password);
}
