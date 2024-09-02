using Trainer.App.Pages;
using Trainer.App.Services;

namespace Trainer.App
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            if (AuthenticationService.IsAuthenticated().Result)
                Application.Current.MainPage = new NavigationPage(new ProfilePage());
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the Registration page
            //await Navigation.PushAsync(new RegistrationPage());
        }

        private async void OnAuthenticateButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the Authentication page
            await Navigation.PushAsync(new AuthenticationPage(Handler.MauiContext));
        }
    }

}
