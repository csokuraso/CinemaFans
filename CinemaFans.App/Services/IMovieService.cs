using CinemaFans.App.Models;

namespace CinemaFans.App.Services;

public interface IMovieService
{
    IReadOnlyList<Movie> GetAll();
    IReadOnlyList<Movie> Search(string text);
    IReadOnlyList<Movie> GetTopMovies(string? genre);
    void AddMovie(User user, Movie movie);
}
