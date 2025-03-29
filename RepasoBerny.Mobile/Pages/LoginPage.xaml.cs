using Microsoft.Maui.Controls;

namespace RepasoBerny.Mobile.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnScreenTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new Register()); 
        }
    }
}
