using CinemaFans.App.Data;
using CinemaFans.App.Services;
using CinemaFans.App.UI;

namespace CinemaFans.App;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        IMovieRepository repository = new InMemoryMovieRepository();
        IAuthService authService = new AuthService();
        IMovieService movieService = new MovieService(repository);
        IReviewService reviewService = new ReviewService(repository);

        Application.Run(new MainForm(authService, movieService, reviewService));
    }
}
