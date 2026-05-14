using CinemaFans.App.Models;

namespace CinemaFans.App.Data;

public sealed class InMemoryMovieRepository : IMovieRepository
{
    private readonly List<Movie> _movies = new()
    {
        new Movie
        {
            Title = "Interstellar",
            Director = "Christopher Nolan",
            Actors = "Matthew McConaughey, Anne Hathaway",
            Budget = 165_000_000,
            ReleaseDate = new DateTime(2014, 11, 7),
            Genre = "Sci-Fi",
            Synopsis = "Подорож крізь космос у пошуках нового дому для людства."
        },
        new Movie
        {
            Title = "The Godfather",
            Director = "Francis Ford Coppola",
            Actors = "Marlon Brando, Al Pacino",
            Budget = 6_000_000,
            ReleaseDate = new DateTime(1972, 3, 24),
            Genre = "Crime",
            Synopsis = "Історія родини Корлеоне та світу організованої злочинності."
        }
    };

    public IReadOnlyList<Movie> GetAll() => _movies.ToList();

    public Movie? GetById(Guid id) => _movies.FirstOrDefault(m => m.Id == id);

    public void Add(Movie movie) => _movies.Add(movie);

    public void AddReview(Guid movieId, Review review)
    {
        Movie? movie = GetById(movieId);
        movie?.Reviews.Add(review);
    }
}
