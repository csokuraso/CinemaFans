namespace CinemaFans.App.Models;

public sealed class Movie
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Actors { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTime ReleaseDate { get; set; } = DateTime.Today;
    public string Genre { get; set; } = string.Empty;
    public string Synopsis { get; set; } = string.Empty;
    public List<Review> Reviews { get; } = new();

    public double AverageRating => Reviews.Count == 0 ? 0 : Reviews.Average(r => r.Rating);
}
