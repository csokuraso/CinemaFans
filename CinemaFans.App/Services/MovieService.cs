using CinemaFans.App.Data;
using CinemaFans.App.Models;

namespace CinemaFans.App.Services;

public sealed class MovieService : IMovieService
{
    private readonly IMovieRepository _repository;

    public MovieService(IMovieRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<Movie> GetAll() => _repository.GetAll();

    public IReadOnlyList<Movie> Search(string text)
    {
        text = text.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(text)) return GetAll();

        return _repository.GetAll()
            .Where(m => m.Title.ToLowerInvariant().Contains(text)
                     || m.Director.ToLowerInvariant().Contains(text)
                     || m.Genre.ToLowerInvariant().Contains(text)
                     || m.Actors.ToLowerInvariant().Contains(text))
            .ToList();
    }

    public IReadOnlyList<Movie> GetTopMovies(string? genre)
    {
        IEnumerable<Movie> query = _repository.GetAll();
        if (!string.IsNullOrWhiteSpace(genre) && genre != "Усі")
            query = query.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));

        return query.OrderByDescending(m => m.AverageRating).ThenBy(m => m.Title).ToList();
    }

    public void AddMovie(User user, Movie movie)
    {
        if (user.Role != UserRole.Admin)
            throw new UnauthorizedAccessException("Додавати фільми може тільки адміністратор.");

        ValidateMovie(movie);
        _repository.Add(movie);
    }

    private static void ValidateMovie(Movie movie)
    {
        if (string.IsNullOrWhiteSpace(movie.Title))
            throw new ArgumentException("Назва фільму є обов'язковою.");
        if (string.IsNullOrWhiteSpace(movie.Genre))
            throw new ArgumentException("Жанр є обов'язковим.");
        if (movie.Budget < 0)
            throw new ArgumentException("Бюджет не може бути від'ємним.");
    }
}
