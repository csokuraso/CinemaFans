namespace CinemaFans.App.UI;

public sealed class LoginForm : Form
{
    private readonly TextBox _loginBox = new() { Width = 180 };
    private readonly TextBox _passwordBox = new() { Width = 180, UseSystemPasswordChar = true };

    public string Login => _loginBox.Text;
    public string Password => _passwordBox.Text;

    public LoginForm()
    {
        Text = "Вхід";
        Width = 320;
        Height = 180;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 3, ColumnCount = 2 };
        panel.Controls.Add(new Label { Text = "Логін:" }, 0, 0);
        panel.Controls.Add(_loginBox, 1, 0);
        panel.Controls.Add(new Label { Text = "Пароль:" }, 0, 1);
        panel.Controls.Add(_passwordBox, 1, 1);

        var ok = new Button { Text = "OK", DialogResult = DialogResult.OK };
        var cancel = new Button { Text = "Скасувати", DialogResult = DialogResult.Cancel };
        panel.Controls.Add(ok, 0, 2);
        panel.Controls.Add(cancel, 1, 2);
        Controls.Add(panel);
    }
}
