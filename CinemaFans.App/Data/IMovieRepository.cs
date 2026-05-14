using CinemaFans.App.Models;

namespace CinemaFans.App.Data;

public interface IMovieRepository
{
    IReadOnlyList<Movie> GetAll();
    Movie? GetById(Guid id);
    void Add(Movie movie);
    void AddReview(Guid movieId, Review review);
}
