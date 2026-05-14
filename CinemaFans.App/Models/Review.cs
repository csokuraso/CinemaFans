namespace CinemaFans.App.Models;

public sealed class Review
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string UserLogin { get; init; } = string.Empty;
    public int Rating { get; init; }
    public string Text { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}
