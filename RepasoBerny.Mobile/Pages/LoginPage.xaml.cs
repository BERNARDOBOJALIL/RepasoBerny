namespace RepasoBerny.Mobile.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Aquí podrías validar usuario/contraseña
        bool loginSuccess = true; // Simulación

        if (loginSuccess)
        {
            // Cambia al Shell
            var app = Application.Current as App;
            app?.NavigateToShell();
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }
}
