using CinemaFans.App.Models;
using CinemaFans.App.Services;

namespace CinemaFans.App.UI;

public sealed class MainForm : Form
{
    private readonly IAuthService _authService;
    private readonly IMovieService _movieService;
    private readonly IReviewService _reviewService;

    private User? _currentUser;
    private readonly DataGridView _grid = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
    private readonly TextBox _searchBox = new() { Width = 220 };
    private readonly ComboBox _genreBox = new() { Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly Label _userLabel = new() { AutoSize = true, Text = "Не авторизовано" };

    public MainForm(IAuthService authService, IMovieService movieService, IReviewService reviewService)
    {
        _authService = authService;
        _movieService = movieService;
        _reviewService = reviewService;

        Text = "Підтримка поціновувачів кінематографу";
        Width = 1880;
        Height = 1000;
        StartPosition = FormStartPosition.CenterScreen;

        BuildUi();
        RefreshGenres();
        RefreshGrid(_movieService.GetAll());
    }

    private void BuildUi()
    {
        var panel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(8) };

        var loginButton = new Button { Text = "Увійти", Width = 100 };
        loginButton.Click += (_, _) => Login();

        var searchButton = new Button { Text = "Знайти", Width = 100 };
        searchButton.Click += (_, _) => RefreshGrid(_movieService.Search(_searchBox.Text));

        var addMovieButton = new Button { Text = "Додати фільм", Width = 130 };
        addMovieButton.Click += (_, _) => AddMovie();

        var reviewButton = new Button { Text = "Додати відгук", Width = 130 };
        reviewButton.Click += (_, _) => AddReview();

        var topButton = new Button { Text = "Рейтинг", Width = 110 };
        topButton.Click += (_, _) => RefreshGrid(_movieService.GetTopMovies(_genreBox.Text));

        panel.Controls.AddRange(new Control[]
        {
            _userLabel, loginButton,
            new Label { Text = "Пошук:", AutoSize = true, Padding = new Padding(12, 7, 0, 0) }, _searchBox, searchButton,
            new Label { Text = "Жанр:", AutoSize = true, Padding = new Padding(12, 7, 0, 0) }, _genreBox, topButton,
            addMovieButton, reviewButton
        });

        Controls.Add(_grid);
        Controls.Add(panel);
    }

    private void Login()
    {
        using var form = new LoginForm();
        if (form.ShowDialog() != DialogResult.OK) return;

        try
        {
            _currentUser = _authService.Login(form.Login, form.Password);
            _userLabel.Text = $"Користувач: {_currentUser.Login} ({_currentUser.Role})";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AddMovie()
    {
        if (_currentUser is null)
        {
            MessageBox.Show("Спочатку увійдіть у систему.");
            return;
        }

        using var form = new MovieForm();
        if (form.ShowDialog() != DialogResult.OK) return;

        try
        {
            _movieService.AddMovie(_currentUser, form.Movie);
            RefreshGenres();
            RefreshGrid(_movieService.GetAll());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AddReview()
    {
        if (_currentUser is null)
        {
            MessageBox.Show("Спочатку увійдіть у систему.");
            return;
        }
        if (_grid.CurrentRow?.Tag is not Movie movie)
        {
            MessageBox.Show("Оберіть фільм у таблиці.");
            return;
        }

        using var form = new ReviewForm(movie.Title);
        if (form.ShowDialog() != DialogResult.OK) return;

        try
        {
            _reviewService.AddReview(_currentUser, movie.Id, form.Rating, form.ReviewText);
            RefreshGrid(_movieService.GetAll());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RefreshGenres()
    {
        string selected = _genreBox.Text;
        _genreBox.Items.Clear();
        _genreBox.Items.Add("Усі");
        foreach (string genre in _movieService.GetAll().Select(m => m.Genre).Distinct().OrderBy(g => g))
            _genreBox.Items.Add(genre);
        _genreBox.SelectedItem = _genreBox.Items.Contains(selected) ? selected : "Усі";
    }

    private void RefreshGrid(IReadOnlyList<Movie> movies)
    {
        _grid.Columns.Clear();
        _grid.Rows.Clear();
        _grid.Columns.Add("Title", "Назва");
        _grid.Columns.Add("Director", "Режисер");
        _grid.Columns.Add("Actors", "Актори");
        _grid.Columns.Add("Budget", "Бюджет");
        _grid.Columns.Add("ReleaseDate", "Дата випуску");
        _grid.Columns.Add("Genre", "Жанр");
        _grid.Columns.Add("Average", "Середня оцінка");
        _grid.Columns.Add("Synopsis", "Синопсис");

        foreach (Movie movie in movies)
        {
            int rowIndex = _grid.Rows.Add(movie.Title, movie.Director, movie.Actors, movie.Budget,
                movie.ReleaseDate.ToShortDateString(), movie.Genre, movie.AverageRating.ToString("0.00"), movie.Synopsis);
            _grid.Rows[rowIndex].Tag = movie;
        }
    }
}
