using CinemaFans.App.Models;

namespace CinemaFans.App.Services;

public interface IReviewService
{
    void AddReview(User user, Guid movieId, int rating, string text);
}
