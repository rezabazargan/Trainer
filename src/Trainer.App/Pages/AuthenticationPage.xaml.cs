using Trainer.App.Services;

namespace Trainer.App.Pages;

public partial class AuthenticationPage : ContentPage
{
    private readonly AuthenticationService? _authenticationService;

    public AuthenticationPage(IMauiContext mauiContext)
    {
        InitializeComponent();
        _authenticationService = mauiContext.Services.GetService<AuthenticationService>();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter both username and password.", "OK");
            return;
        }

        var isAuthenticated = await _authenticationService.LoginAsync(username, password);
        if (isAuthenticated)
        {
            // Navigate to the Profile page on successful login
            Application.Current.MainPage = new NavigationPage(new ProfilePage());
        }
        else
        {
            await DisplayAlert("Error", "Login failed. Please check your credentials and try again.", "OK");
        }
    }
}