using CinemaFans.App.Data;
using CinemaFans.App.Models;

namespace CinemaFans.App.Services;

public sealed class ReviewService : IReviewService
{
    private readonly IMovieRepository _repository;

    public ReviewService(IMovieRepository repository)
    {
        _repository = repository;
    }

    public void AddReview(User user, Guid movieId, int rating, string text)
    {
        if (rating < 1 || rating > 10)
            throw new ArgumentException("Оцінка повинна бути від 1 до 10.");
        if (text.Length > 1000)
            throw new ArgumentException("Відгук занадто довгий.");
        if (_repository.GetById(movieId) is null)
            throw new ArgumentException("Фільм не знайдено.");

        _repository.AddReview(movieId, new Review
        {
            UserLogin = user.Login,
            Rating = rating,
            Text = text.Trim()
        });
    }
}
