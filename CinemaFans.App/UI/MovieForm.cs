using CinemaFans.App.Models;

namespace CinemaFans.App.UI;

public sealed class MovieForm : Form
{
    private readonly TextBox _title = new() { Width = 250 };
    private readonly TextBox _director = new() { Width = 250 };
    private readonly TextBox _actors = new() { Width = 250 };
    private readonly NumericUpDown _budget = new() { Maximum = 1_000_000_000, Width = 250 };
    private readonly DateTimePicker _date = new() { Width = 250 };
    private readonly TextBox _genre = new() { Width = 250 };
    private readonly TextBox _synopsis = new() { Width = 250, Height = 80, Multiline = true };

    public Movie Movie => new()
    {
        Title = _title.Text.Trim(),
        Director = _director.Text.Trim(),
        Actors = _actors.Text.Trim(),
        Budget = _budget.Value,
        ReleaseDate = _date.Value.Date,
        Genre = _genre.Text.Trim(),
        Synopsis = _synopsis.Text.Trim()
    };

    public MovieForm()
    {
        Text = "Додавання фільму";
        Width = 430;
        Height = 430;
        StartPosition = FormStartPosition.CenterParent;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 8, ColumnCount = 2 };
        AddRow(panel, "Назва:", _title, 0);
        AddRow(panel, "Режисер:", _director, 1);
        AddRow(panel, "Актори:", _actors, 2);
        AddRow(panel, "Бюджет:", _budget, 3);
        AddRow(panel, "Дата випуску:", _date, 4);
        AddRow(panel, "Жанр:", _genre, 5);
        AddRow(panel, "Синопсис:", _synopsis, 6);

        panel.Controls.Add(new Button { Text = "Зберегти", DialogResult = DialogResult.OK }, 0, 7);
        panel.Controls.Add(new Button { Text = "Скасувати", DialogResult = DialogResult.Cancel }, 1, 7);
        Controls.Add(panel);
    }

    private static void AddRow(TableLayoutPanel panel, string text, Control control, int row)
    {
        panel.Controls.Add(new Label { Text = text, AutoSize = true }, 0, row);
        panel.Controls.Add(control, 1, row);
    }
}
