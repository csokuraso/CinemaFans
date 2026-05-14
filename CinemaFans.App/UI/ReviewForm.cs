namespace CinemaFans.App.UI;

public sealed class ReviewForm : Form
{
    private readonly NumericUpDown _rating = new() { Minimum = 1, Maximum = 10, Value = 8, Width = 180 };
    private readonly TextBox _text = new() { Width = 260, Height = 100, Multiline = true };

    public int Rating => (int)_rating.Value;
    public string ReviewText => _text.Text;

    public ReviewForm(string movieTitle)
    {
        Text = $"Відгук: {movieTitle}";
        Width = 400;
        Height = 260;
        StartPosition = FormStartPosition.CenterParent;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 3, ColumnCount = 2 };
        panel.Controls.Add(new Label { Text = "Оцінка 1-10:" }, 0, 0);
        panel.Controls.Add(_rating, 1, 0);
        panel.Controls.Add(new Label { Text = "Відгук:" }, 0, 1);
        panel.Controls.Add(_text, 1, 1);
        panel.Controls.Add(new Button { Text = "Зберегти", DialogResult = DialogResult.OK }, 0, 2);
        panel.Controls.Add(new Button { Text = "Скасувати", DialogResult = DialogResult.Cancel }, 1, 2);
        Controls.Add(panel);
    }
}
