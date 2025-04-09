namespace RepasoBerny.Mobile.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Aqu� podr�as validar usuario/contrase�a
        bool loginSuccess = true; // Simulaci�n

        if (loginSuccess)
        {
            // Cambia al Shell
            var app = Application.Current as App;
            app?.NavigateToShell();
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contrase�a incorrectos", "OK");
        }
    }
}
